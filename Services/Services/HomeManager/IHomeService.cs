using System;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.ServiceDto_s.Responses;

namespace CourseApi.Service.Services.HomeManager;

public interface IHomeService
{
    Task<BaseApiResponse<IEnumerable<CarouselItem>>> GetCarouselItems();
}
