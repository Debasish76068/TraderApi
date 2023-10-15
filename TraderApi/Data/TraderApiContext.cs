using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraderApi.Models;
using TraderApi.Data.Entities;

namespace TraderApi.Data
{
    public class TraderApiContext : DbContext
    {
        public TraderApiContext (DbContextOptions<TraderApiContext> options)
            : base(options)
        {
        }
       
        public DbSet<TraderApi.Data.Entities.Purchaser>? Purchaser { get; set; }
       
        public DbSet<TraderApi.Data.Entities.Agent>? Agent { get; set; }
       
        public DbSet<TraderApi.Data.Entities.Order>? Order { get; set; }
       
        public DbSet<TraderApi.Data.Entities.Transporter>? Transporter { get; set; }
    }
}
