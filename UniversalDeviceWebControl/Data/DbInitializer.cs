using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using UniversalDeviceWebControl.Models;
using UniversalDeviceWebControl.Data;

namespace UniversalDeviceWebControl.Data
{
    public class DbInitializer
    {
        private readonly WebDBContext _context;

        public void Initialize()
        {
            //如果数据库未初始化，默认24个pin
            if (_context.Setting.Any(s => s.Key == "GPIO Num"))
            {
                _context.Setting.Add(new Setting { Key = "GPIO Num", Value = "24", Type = "Hardware", Description = "GPIO数量", Editable = false });
                _context.SaveChanges();

                //初始化GPIO
                for (int i = 1; i <= 24; i++)
                {
                    _context.GPIO.Add(new GPIO { Pin = i, Type = "Out", CurrentValue = 0 });
                    //有关文件操作
                    FileStream fs = new FileStream($"./GPIO/GPIO-{i}.txt", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write("0");
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                    //
                    _context.SaveChanges();
                }
            }
            else
            {

            }
        }
    }
}
