﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectionData.Data.Entities;

namespace ElectionData.Data
{
    public class ElectionDataDbContext : DbContext
    {
        public ElectionDataDbContext(DbContextOptions<ElectionDataDbContext> options) : base(options)
        {

        }

        public DbSet<CleanPoll> Polls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CleanPoll>()
                .HasIndex(p => p.PollLink)
                .IsUnique();
        }

        public void ApplyMigrations()
        {
            this.Database.Migrate();
        }
    }

}
