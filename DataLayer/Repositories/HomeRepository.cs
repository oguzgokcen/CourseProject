using System;
using CourseApi.DataLayer.DataContext;
using CourseApi.DataLayer.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.DataLayer.Repositories;

public class HomeRepository(CourseDbContext _context) : IHomeRepository
{
    public async Task<IEnumerable<CarouselItem>> GetCarouselItems()
    {
        return await _context.CarouselItems.ToListAsync();
    }
}
