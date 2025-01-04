using CourseApi.DataLayer.DataContext;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CourseApi.DataLayer.Initializer
{
	public static class DbInitializer
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

				var categories = new List<CategoryKeywords>
					{
						new CategoryKeywords { Keyword = "Programming", SearchTerm = "programming" },
						new CategoryKeywords { Keyword = "Business", SearchTerm = "business" },
						new CategoryKeywords { Keyword = "Design", SearchTerm = "design" },
						new CategoryKeywords { Keyword = "Data Science", SearchTerm = "data-science" },
						new CategoryKeywords { Keyword = "Web Development", SearchTerm = "web-development" },
						new CategoryKeywords { Keyword = "Machine Learning", SearchTerm = "machine-learning" }
					};

				context.CategoryKeywords.AddRange(categories);
				await context.SaveChangesAsync();

				var courses = new List<Course>
					{
                        
                        new Course
						{
							Title = "Introduction to Programming",
							DescriptionHeader = "Learn the basics of programming",
							PublishedDate = DateTime.Now,
							Price = 49.99m,
							Language = Language.English,
							Rating = 4.5f,
							NumberOfRatings = 100,
							NumberOfStudents = 1000,
							Description = "This course covers the basics of programming including variables, loops, and functions.",
							Categories = new List<CategoryKeywords> { categories.First(c => c.SearchTerm == "programming") },
							InstructorId = teacher.Id,
							Instructor = teacher,
							ImageUrl = "https://img-c.udemycdn.com/course/240x135/3934228_e9fe_5.jpg"
						},
						new Course
						{
							Title = "Advanced C# Programming",
							DescriptionHeader = "Master advanced concepts in C#",
							PublishedDate = DateTime.Now,
							Price = 99.99m,
							Language = Language.English,
							Rating = 4.8f,
							NumberOfRatings = 200,
							NumberOfStudents = 500,
							Description = "This course covers advanced topics in C# including LINQ, async/await, and design patterns.",
							Categories = new List<CategoryKeywords> { categories.First(c => c.SearchTerm == "programming") },
							InstructorId = teacher.Id,
							Instructor = teacher,
						},

                        // New Courses

                        // Programming Courses
                        new Course
						{
							Title = "Python for Data Science",
							DescriptionHeader = "Utilize Python in data analysis",
							PublishedDate = DateTime.Now,
							Price = 59.99m,
							Language = Language.English,
							Rating = 4.6f,
							NumberOfRatings = 150,
							NumberOfStudents = 750,
							Description = "Learn how to apply Python in data science projects.",
							Categories = new List<CategoryKeywords>
							{
								categories.First(c => c.SearchTerm == "programming"),
								categories.First(c => c.SearchTerm == "data-science"),
								categories.First(c => c.SearchTerm == "machine-learning")
							},
							InstructorId = teacher.Id,
							Instructor = teacher,
						},
						new Course
						{
							Title = "JavaScript Essentials",
							DescriptionHeader = "Master the fundamentals of JavaScript",
							PublishedDate = DateTime.Now,
							Price = 39.99m,
							Language = Language.English,
							Rating = 4.4f,
							NumberOfRatings = 120,
							NumberOfStudents = 600,
							Description = "A comprehensive guide to JavaScript for beginners.",
							Categories = new List<CategoryKeywords>
							{
								categories.First(c => c.SearchTerm == "programming"),
								categories.First(c => c.SearchTerm == "web-development")
							},
							InstructorId = teacher.Id,
							Instructor = teacher,
						},
						new Course
						{
							Title = "Full-Stack Web Development",
							DescriptionHeader = "Become a full-stack developer",
							PublishedDate = DateTime.Now,
							Price = 89.99m,
							Language = Language.English,
							Rating = 4.7f,
							NumberOfRatings = 180,
							NumberOfStudents = 900,
							Description = "Learn both front-end and back-end web development.",
							Categories = new List<CategoryKeywords>
							{
								categories.First(c => c.SearchTerm == "programming"),
								categories.First(c => c.SearchTerm == "web-development")
							},
							InstructorId = teacher.Id,
							Instructor = teacher,
							ImageUrl = "https://img-c.udemycdn.com/course/240x135/3934228_e9fe_5.jpg"
						},

                        // Business Courses
                        new Course
						{
							Title = "Business Strategy 101",
							DescriptionHeader = "Develop effective business strategies",
							PublishedDate = DateTime.Now,
							Price = 79.99m,
							Language = Language.English,
							Rating = 4.5f,
							NumberOfRatings = 130,
							NumberOfStudents = 650,
							Description = "Learn the key components of successful business strategies.",
							Categories = new List<CategoryKeywords> { categories.First(c => c.SearchTerm == "business") },
							InstructorId = teacher.Id,
							Instructor = teacher,
						},
						new Course
						{
							Title = "Digital Marketing Basics",
							DescriptionHeader = "Introduction to digital marketing",
							PublishedDate = DateTime.Now,
							Price = 45.99m,
							Language = Language.Spanish,
							Rating = 4.2f,
							NumberOfRatings = 110,
							NumberOfStudents = 550,
							Description = "Understand the fundamentals of digital marketing.",
							Categories = new List<CategoryKeywords> { categories.First(c => c.SearchTerm == "business") },
							InstructorId = teacher.Id,
							Instructor = teacher,
						},
						new Course
						{
							Title = "Entrepreneurship Essentials",
							DescriptionHeader = "Start and grow your own business",
							PublishedDate = DateTime.Now,
							Price = 99.99m,
							Language = Language.English,
							Rating = 4.9f,
							NumberOfRatings = 220,
							NumberOfStudents = 1100,
							Description = "A complete guide to launching and managing a successful business.",
							Categories = new List<CategoryKeywords> { categories.First(c => c.SearchTerm == "business") },
							InstructorId = teacher.Id,
							Instructor = teacher,
						},
						new Course
						{
							Title = "Personal Finance Management",
							DescriptionHeader = "Manage your personal finances effectively",
							PublishedDate = DateTime.Now,
							Price = 49.99m,
							Language = Language.English,
							Rating = 4.1f,
							NumberOfRatings = 90,
							NumberOfStudents = 450,
							Description = "Learn how to budget, save, and invest your money wisely.",
							Categories = new List<CategoryKeywords> { categories.First(c => c.SearchTerm == "business") },
							InstructorId = teacher.Id,
							Instructor = teacher,
						},

                        // Design Courses
                        new Course
						{
							Title = "UI/UX Design Principles",
							DescriptionHeader = "Design intuitive user interfaces",
							PublishedDate = DateTime.Now,
							Price = 69.99m,
							Language = Language.English,
							Rating = 4.6f,
							NumberOfRatings = 140,
							NumberOfStudents = 700,
							Description = "Learn the fundamentals of UI/UX design.",
							Categories = new List<CategoryKeywords> { categories.First(c => c.SearchTerm == "design") },
							InstructorId = teacher.Id,
							Instructor = teacher,
						},
						new Course
						{
							Title = "Mastering Graphic Design",
							DescriptionHeader = "Advanced techniques in graphic design",
							PublishedDate = DateTime.Now,
							Price = 59.99m,
							Language = Language.French,
							Rating = 4.4f,
							NumberOfRatings = 100,
							NumberOfStudents = 500,
							Description = "Enhance your graphic design skills with advanced tools and techniques.",
							Categories = new List<CategoryKeywords> { categories.First(c => c.SearchTerm == "design") },
							InstructorId = teacher.Id,
							Instructor = teacher,
						},
						new Course
						{
							Title = "Creative Web Design",
							DescriptionHeader = "Design visually appealing websites",
							PublishedDate = DateTime.Now,
							Price = 54.99m,
							Language = Language.English,
							Rating = 4.3f,
							NumberOfRatings = 95,
							NumberOfStudents = 475,
							Description = "Learn how to create beautiful and responsive web designs.",
							Categories = new List<CategoryKeywords> { categories.First(c => c.SearchTerm == "design") },
							InstructorId = teacher.Id,
							Instructor = teacher,
						}
					};

				context.Courses.AddRange(courses);
				await context.SaveChangesAsync();
			}
		}
	}
}
