﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseApi.DataLayer.Enums;

namespace CourseApi.DataLayer.ServiceDto_s.Requests.Course
{
	public record SearchCourseRequest(int? PageNumber, int? PageSize, string? Keyword = "", double? MinRating = null, Language? Language = null);
}
