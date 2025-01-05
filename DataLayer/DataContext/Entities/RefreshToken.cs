using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApi.DataLayer.DataContext.Entities
{
	public class RefreshToken
	{
		public Guid Id { get; set; }
		public string Token { get; set; }
		public DateTime ExpiresOnUtc { get; set; }
		public Guid UserId { get; set; }
		public AppUser User { get; set; }
		
	}
}
