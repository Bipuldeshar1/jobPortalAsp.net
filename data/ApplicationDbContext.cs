﻿using jobPortal.Models;
using jobPortal.Models.category;
using jobPortal.Models.job;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace jobPortal.data
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<JobModel> JobModels { get; set; }

        public DbSet<ApplyModel> ApplyModels { get; set; }

        public DbSet<CategoryModel> CategoriesModel {  get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<JobModel>()
                 .HasOne(j => j.appUser)
                 .WithMany(u => u.JobModels)
                 .HasForeignKey(j => j.AuthorId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<JobModel>()
          .HasOne(j => j.Category)
          .WithMany(c => c.jobModels)
          .HasForeignKey(j => j.CategoryId);
       

            builder.Entity<ApplyModel>()
                .HasOne(a=>a.JobModel)
                .WithMany(j=>j.applyModels)
                .HasForeignKey(a=>a.JobId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        
    }
}
