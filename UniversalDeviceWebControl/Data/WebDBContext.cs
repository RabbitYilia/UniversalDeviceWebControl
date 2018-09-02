using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversalDeviceWebControl.Models;

namespace UniversalDeviceWebControl.Data
{
    public class WebDBContext : DbContext
    {
        public DbSet<Setting> Setting { get; set; }
        public DbSet<GPIO> GPIO { get; set; }

        public WebDBContext(DbContextOptions<WebDBContext> options) : base(options)
        {
        }

    }
}
