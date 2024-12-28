using CourseApi.DataLayer.DataContext.Entities;
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

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);


		}
	}
}
