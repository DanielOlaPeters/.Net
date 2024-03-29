﻿using Lab4P1.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab4P1.Data
{
    public class SportsDbContext : DbContext
    {
        public SportsDbContext(DbContextOptions<SportsDbContext> options) : base(options) { }
        public DbSet<Fan> Fans { get; set; }
        public DbSet<SportClub> SportClubs { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Fan>().ToTable("Fan");
        modelBuilder.Entity<SportClub>().ToTable("SportClub");
        modelBuilder.Entity<Subscription>().ToTable("Subscription");
        modelBuilder.Entity<Subscription>().HasKey(s => new { s.FanID, s.SportClubID });
        }
    }
}
