﻿using CaseStudyBooksAPI.DAL.DomainClasses;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyBooksAPI.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Product> Products { get; set; }
    }
}
