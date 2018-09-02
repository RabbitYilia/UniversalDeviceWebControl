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
                _context.Setting.Add(new Setting {
                    Key = "GPIO Num",
                    Value = "24",
                    Type = "Hardware",
                    Description = "GPIO数量",
                    Editable = false
                });
                _context.SaveChanges();

                //初始化GPIO
                for (int i = 1; i <= 24; i++)
                {
                    _context.GPIO.Add(new GPIO { Pin = i, Type = "Out", CurrentValue = 0 });
                    //有关文件操作
                    File.WriteAllText($"./GPIO/GPIO-{i}.txt","0");
                    //
                    _context.SaveChanges();
                }
            }
            else
            {
                var GPIOs = _context.GPIO.ToList();
                _context.Setting.Single(s => s.Key == "GPIO Num").Value = GPIOs.Count.ToString();
                _context.SaveChanges();
                foreach(GPIO gpio in GPIOs)
                {
                    //输出模式则赋值
                    if(gpio.Type=="Out")
                    {
                        //文件操作
                        File.WriteAllText($"./GPIO/GPIO-{gpio.Pin}.txt", "0");
                        //
                    }
                    else
                    {
                        //输入模式则读取
                        gpio.CurrentValue = int.Parse(File.ReadAllText($"./GPIO/GPIO-{gpio.Pin}.txt"));
                        _context.SaveChanges();
                    }
                }
            }
        }
    }
}
