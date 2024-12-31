using CourseApi.DataLayer.DataContext;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CourseApi.DataLayer.Initializer
{
	public class DbInitializer
	{
		public static void InitDb(WebApplication app)
		{
			using var scope = app.Services.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

			SeedData(context, userManager, roleManager).Wait();
		}

		private static async Task SeedData(CourseDbContext context, UserManager<AppUser> _userManager, RoleManager<AppRole> _roleManager)
		{
			//context.Database.Migrate();

			var users = new[]
				{
					new AppUser
					{
						UserName = "teacher@example.com",
						Email = "teacher@example.com",
						FullName = "Teacher User"
					},
					new AppUser
					{
						UserName = "user@example.com",
						Email = "user@example.com",
						FullName = "Regular User"
					}
				};

			if (!context.Users.Any())
			{

				foreach (var user in users)
				{
					var result = await _userManager.CreateAsync(user, "Password123!");
					if (!result.Succeeded)
					{
						throw new Exception($"Failed to create user {user.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
					}
				}
			}
			if (!context.Roles.Any())
			{

				var roles = new[]
				{
					new AppRole()
					{
						Name = "Teacher"
					}
				};

				foreach (var role in roles)
				{
					var result = await _roleManager.CreateAsync(role);
					if (!result.Succeeded)
					{
						throw new Exception($"Failed to create role {role.Name}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
					}
				}
				var user = await _userManager.FindByEmailAsync(users[0].Email!);

				var result2 = await _userManager.AddToRoleAsync(user, "Teacher");
				if (!result2.Succeeded)
				{
					throw new Exception($"Failed to add user {users[0].UserName} to role Teacher: {string.Join(", ", result2.Errors.Select(e => e.Description))}");
				}
			}

			if (!context.Courses.Any())
			{
				var teacher = await _userManager.FindByEmailAsync("teacher@example.com");

				var courses = new[]
				{
					new Course
					{
						Title = "Introduction to Programming",
						DescriptionHeader = "Learn the basics of programming",
						PublishedDate = DateTime.Now,
						Price = 49.99m,
						Language = Language.English,
						Rating = 4.5,
						NumberOfRatings = 100,
						NumberOfStudents = 1000,
						Description = "This course covers the basics of programming including variables, loops, and functions.",
						Categories = new List<CategoryKeywords> { new CategoryKeywords { Keyword = "Programming", SearchTerm ="programming-languages" } },
						InstructorId = teacher.Id,
						Instructor = teacher
					},
					new Course
					{
						Title = "Advanced C# Programming",
						DescriptionHeader = "Master advanced concepts in C#",
						PublishedDate = DateTime.Now,
						Price = 99.99m,
						Language = Language.English,
						Rating = 4.8,
						NumberOfRatings = 200,
						NumberOfStudents = 500,
						Description = "This course covers advanced topics in C# including LINQ, async/await, and design patterns.",
						Categories = new List<CategoryKeywords> { new CategoryKeywords { Keyword = "C#", SearchTerm = "c-sharp" } },
						InstructorId = teacher.Id,
						Instructor = teacher
					}
				};

				context.Courses.AddRange(courses);
				await context.SaveChangesAsync();
			}
		}
	}
}
