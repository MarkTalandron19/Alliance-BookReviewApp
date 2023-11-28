using ASI.Basecode.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ASI.Basecode.WebApp.Models
{
	public class IdentityUserViewModel
	{
		public User User { get; set; }
		public List<string>	UserRoles { get; set; }
	}
}
