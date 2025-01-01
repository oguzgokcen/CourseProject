using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.DataContext;

namespace CourseApi.Service.UoW
{
    public class UnitOfWork(CourseDbContext _dbContext) : IUnitOfWork
    {
        public void SaveChanges()
		{
			_dbContext.SaveChanges();
		}

		public async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}
	}
}