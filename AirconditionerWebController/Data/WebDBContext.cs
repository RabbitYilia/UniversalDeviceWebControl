using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AirconditionerWebController.Models;

namespace AirconditionerWebController.Data
{
    public class WebDBContext : DbContext
    {
        public DbSet<Setting> Setting { get; set; }

        public WebDBContext(DbContextOptions<WebDBContext> options) : base(options)
        {
        }

    }
}
