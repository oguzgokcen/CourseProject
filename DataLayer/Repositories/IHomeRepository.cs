using System;
using CourseApi.DataLayer.DataContext.Entities;

namespace CourseApi.DataLayer.Repositories;

public interface IHomeRepository
{
    Task<IEnumerable<CarouselItem>> GetCarouselItems();
}
