﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mini_blog.Entities;
using mini_blog.Enum;

namespace mini_blog.DataAccess
{
    public class BlogContext : IdentityDbContext<AppUser>
    {
        public BlogContext(DbContextOptions options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Blog>().Property(b => b.Status).HasConversion(
                o => o.ToString(),
                o => (BlogStatus)System.Enum.Parse(typeof(BlogStatus), o)
            );

        }
    }
}