﻿using System;
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
       
        public DbSet<TraderApi.Data.Entities.AgentOrder>? AgentOrder { get; set; }
        public DbSet<TraderApi.Data.Entities.PurchaserOrder>? PurchaserOrder { get; set; }

        public DbSet<TraderApi.Data.Entities.Transporter>? Transporter { get; set; }
       
        public DbSet<TraderApi.Data.Entities.GstDetail>? GstDetail { get; set; }
       
        public DbSet<TraderApi.Data.Entities.AccountDetail>? AccountDetail { get; set; }
       
        public DbSet<TraderApi.Data.Entities.Dispatch>? Dispatch { get; set; }
       
        public DbSet<TraderApi.Data.Entities.Items>? Items { get; set; }
       
        public DbSet<TraderApi.Data.Entities.SalesBill>? SalesBill { get; set; }
       
    }
}
