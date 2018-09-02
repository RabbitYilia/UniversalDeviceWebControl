using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using UniversalDeviceWebControl.Models;
using UniversalDeviceWebControl.Data;
using Microsoft.Extensions.DependencyInjection;

namespace UniversalDeviceWebControl.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var _context = serviceProvider.GetRequiredService<WebDBContext>();
            _context.Database.EnsureCreated();
            //如果数据库未初始化，默认24个pin
            if (!_context.Setting.Any(s => s.Key == "GPIO Num"))
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
                var GPIOs = _context.GPIO.ToList();
                _context.Setting.Single(s => s.Key == "GPIO Num").Value = GPIOs.Count.ToString();
                _context.SaveChanges();
                foreach(GPIO thisGPIO in GPIOs)
                {
                    //输出模式则赋值
                    if(thisGPIO.Type=="Out")
                    {
                        //文件操作
                        FileStream fs = new FileStream($"./GPIO/GPIO-{thisGPIO.Pin}.txt", FileMode.Open);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(thisGPIO.CurrentValue.ToString());
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                        //
                    }
                    else
                    {
                        //输入模式则读取
                        StreamReader sr = new StreamReader($"./GPIO/GPIO-{thisGPIO.Pin}.txt");
                        thisGPIO.CurrentValue = int.Parse(sr.ReadToEnd());
                        _context.SaveChanges();
                    }
                }
            }
        }
    }
}
