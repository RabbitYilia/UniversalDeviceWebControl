using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversalDeviceWebControl.Models
{
    public class GPIO
    {
        public int ID { get; set; }
        public int Pin { get; set; }
        public String Type { get; set; }
        public int CurrentValue { get; set; }
    }
}
