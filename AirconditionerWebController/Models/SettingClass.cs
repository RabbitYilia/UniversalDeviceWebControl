using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirconditionerWebController.Models
{
    public class Setting
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string Useage { get; set; }
    }
}
