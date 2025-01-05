using System;
using CourseApi.DataLayer.DataContext.Entities;
using CourseApi.DataLayer.Enums;
using CourseApi.DataLayer.ServiceDto_s.Responses.Course;

namespace CourseApi.DataLayer.ServiceDto_s.Responses.User;

public record PaymentHistoryItemDto(GetCourseListDto Course, DateTime? BoughtDate);

public class PaymentHistoryDto
{
	public Guid Id { get; set; }
	public DateTime CreatedOnUtc { get; set; }
	public decimal TotalPrice { get; set; }
	public PaymentStatus PaymentStatus { get; set; }
	public List<PaymentHistoryItemDto> BoughtCourses { get; set; } = [];
}
