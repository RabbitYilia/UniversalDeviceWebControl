using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using AirconditionerWebController.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AirconditionerWebController.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var _context = serviceProvider.GetRequiredService<WebDBContext>();
            _context.Database.EnsureCreated();

            if (!_context.Setting.Any(s => s.Key == "Current Temperature"))
            {
                _context.Setting.Add(new Setting
                {
                    Key = "Current Temperature",
                    Value = "N/A",
                    Type = "Status",
                    Description = "当前温度",
                    Useage = "显示当前温度，禁止修改"
                });
                _context.SaveChanges();
            }
            else
            {
                File.WriteAllText($"./Settings/Current-Temp.txt", "N/A");
            }
            if (!_context.Setting.Any(s => s.Key == "Current Relative humitity"))
            {
                _context.Setting.Add(new Setting
                {
                    Key = "Current Relative humitity",
                    Value = "N/A",
                    Type = "Status",
                    Description = "当前相对湿度",
                    Useage = "显示当前相对湿度，禁止修改"
                });
                _context.SaveChanges();
            }
            else
            {
                File.WriteAllText($"./Settings/Current-humitity.txt", "N/A");
            }
            if (!_context.Setting.Any(s => s.Key == "Current PowerState"))
            {
                _context.Setting.Add(new Setting
                {
                    Key = "Current PowerState",
                    Value = "OFF",
                    Type = "Function",
                    Description = "工作状态，OFF为待机，ON为工作",
                    Useage = "修改工作状态以启动设备，OFF为待机，ON为开机"
                });
                _context.SaveChanges();
            }
            else
            {
                File.WriteAllText($"./Settings/Current-PowerState.txt", "OFF");
            }
            if (!_context.Setting.Any(s => s.Key == "Current Mode"))
            {
                _context.Setting.Add(new Setting
                {
                    Key = "Current Mode",
                    Value = "Wind",
                    Type = "Function",
                    Description = "当前状态，Wind为送风，Cool为制冷，Heat为加热",
                    Useage = "修改当前状态以启用对应模式，Wind为送风，Cool为制冷，Heat为加热"
                });
                _context.SaveChanges();
            }
            else
            {
                File.WriteAllText($"./Settings/Current-Mode.txt", "Wind");
            }
            if (!_context.Setting.Any(s => s.Key == "Wind Speed"))
            {
                _context.Setting.Add(new Setting
                {
                    Key = "Wind Speed",
                    Value = "3",
                    Type = "Function",
                    Description = "当前风速，0睡眠模式，1为微风，2为弱风，3为中风，4为中强风，5为强风，6为自动",
                    Useage = "修改当前状态以调整风速，0睡眠模式，1为微风，2为弱风，3为中风，4为中强风，5为强风，6为自动"
                });
                _context.SaveChanges();
            }
            else
            {
                File.WriteAllText($"./Settings/Wind-Speed.txt", "3");
            }
            if (!_context.Setting.Any(s => s.Key == "Wind Vertical Direction"))
            {
                _context.Setting.Add(new Setting
                {
                    Key = "Wind Vertical Direction",
                    Value = "0",
                    Type = "Function",
                    Description = "垂直风向，0为自动，数值为垂直吹风角度，范围从1-90，单位：度",
                    Useage = "垂直风向，0为自动，输入数值固定角度，范围从1-90，单位：度"
                });
                _context.SaveChanges();
            }
            else
            {
                File.WriteAllText($"./Settings/Wind-VDirection.txt", "0");
            }
            if (!_context.Setting.Any(s => s.Key == "Wind Horizontal Direction"))
            {
                _context.Setting.Add(new Setting
                {
                    Key = "Wind Horizontal Direction",
                    Value = "0",
                    Type = "Function",
                    Description = "水平风向，0为自动，数值为垂直吹风角度，范围从1-180，单位：度",
                    Useage = "水平风向，0为自动，输入数值固定角度，范围从1-180，单位：度"
                });
                _context.SaveChanges();
            }
            else
            {
                File.WriteAllText($"./Settings/Wind-HDirection.txt", "0");
            }
            if (!_context.Setting.Any(s => s.Key == "Target Temperature"))
            {
                _context.Setting.Add(new Setting
                {
                    Key = "Target Temperature",
                    Value = "27",
                    Type = "Function",
                    Description = "目标温度，单位：摄氏度",
                    Useage = "修改以设置目标温度，单位：摄氏度"
                });
                _context.SaveChanges();
            }
            else
            {
                File.WriteAllText($"./Settings/Target-Temperature.txt", "27");
            }
        }
    }
}
