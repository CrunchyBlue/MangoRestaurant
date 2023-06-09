﻿using Mango.Services.OrderProcessor.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.OrderProcessor.DbContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
}