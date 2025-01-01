﻿using CourseApi.DataLayer.DataContext.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.DataContext
{
	public class CourseDbContext(DbContextOptions<CourseDbContext> options) : IdentityDbContext<AppUser,AppRole,Guid>(options)
	{

		public DbSet<Course> Courses { get; set; }
		public DbSet<CategoryKeywords> CategoryKeywords { get; set; }

		public DbSet<CartItem> CartItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CategoryKeywords>(entity =>
			{
				entity.HasKey(ck => ck.Id);
				entity.Property(ck => ck.Keyword)
					.IsRequired()
					.HasMaxLength(100);
				entity.Property(ck => ck.SearchTerm)
					.IsRequired()
					.HasMaxLength(50);
				entity.HasIndex(ck => ck.SearchTerm)
					.IsUnique();
			});

			modelBuilder.Entity<Course>(entity =>
			{
				entity.HasKey(c => c.Id);
				entity.Property(c => c.Title)
					.IsRequired()
					.HasMaxLength(50);
				entity.Property(c => c.DescriptionHeader)
					.HasDefaultValue("").HasMaxLength(100);
				entity.Property(c => c.Language)
					.IsRequired();
				entity.Property(c => c.Price)
					.HasColumnType("decimal(10, 2)")
					.IsRequired();
				entity.Property(c => c.Rating)
					.IsRequired();
				entity.Property(c => c.NumberOfRatings)
					.HasDefaultValue(0);
				entity.Property(c => c.NumberOfStudents)
					.HasDefaultValue(0);
				entity.Property(c => c.Rating)
					.HasColumnType("decimal(10, 2)")
					.HasDefaultValue(0.00);
				entity.Property(c => c.PublishedDate)
					.HasColumnType("datetime")
					.IsRequired();
				modelBuilder.Entity<Course>()
					.HasMany(e => e.Categories)
					.WithMany(e => e.Courses);

				modelBuilder.Entity<Course>()
					.HasMany(e => e.Users)
					.WithMany(e => e.BoughtCourses);


				modelBuilder.Entity<Course>()
					.HasOne(e => e.Instructor)
					.WithMany(e => e.CreatedCourses)
					.HasForeignKey(e => e.InstructorId)
					.OnDelete(DeleteBehavior.NoAction)
					.IsRequired();
			});

			modelBuilder.Entity<AppUser>(entity =>
			{
				entity.Property(u => u.FullName)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(u => u.Title)
					.HasDefaultValue("").HasMaxLength(50);

				entity.Property(u => u.Description)
					.HasDefaultValue("").HasMaxLength(200);

				entity.Property(u => u.Website)
					.HasDefaultValue("").HasMaxLength(100);
			});

			modelBuilder.Entity<CartItem>(entity =>
			{
				entity.HasKey(ci => ci.Id);
				entity.Property(ci => ci.CourseId)
					.IsRequired();
				entity.Property(ci => ci.UserId)
					.IsRequired();
				entity.HasOne(ci => ci.Course)
					.WithMany(c => c.CartItems)
					.HasForeignKey(ci => ci.CourseId)
					.OnDelete(DeleteBehavior.NoAction)
					.IsRequired();
				entity.HasOne(ci => ci.User)
					.WithMany(u => u.CartItems)
					.HasForeignKey(ci => ci.UserId)
					.OnDelete(DeleteBehavior.NoAction)
					.IsRequired();
			});


		}
	}
}