using System;
using CourseApi.Cache;
using CourseApi.DataLayer.Repositories;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.Cache.CacheManager;
using CourseApi.DataLayer.ServiceDto_s.Responses;

namespace CourseApi.Service.Services.HomeManager;

public class HomeService(IHomeRepository _homeRepository, ICacheRepository _cacheRepository) : IHomeService
{
    public async Task<BaseApiResponse<IEnumerable<CarouselItem>>> GetCarouselItems()
    {
	    var items = await _cacheRepository.Get<IEnumerable<CarouselItem>>(CacheKeys.CarouselItems);
	    if (items == null)
	    {
		    var dbItems = await _homeRepository.GetCarouselItems();
			await _cacheRepository.Set(CacheKeys.CarouselItems, dbItems);
			return BaseApiResponse<IEnumerable<CarouselItem>>.Success(dbItems);
		}
		return BaseApiResponse<IEnumerable<CarouselItem>>.Success(items);
	
	}
}
