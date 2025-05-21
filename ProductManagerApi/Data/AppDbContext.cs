﻿using Microsoft.EntityFrameworkCore;
using ProductManagerApi.Models;

namespace ProductManagerApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<UserModel> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
    }
}