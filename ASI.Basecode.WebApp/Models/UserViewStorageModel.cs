using ASI.Basecode.Services.ServiceModels;
using System.Collections.Generic;

namespace ASI.Basecode.WebApp.Models
{
    public class UserViewStorageModel
    {
        public UserViewModel ViewModel { get; set; }
        public List<IdentityUserViewModel> IdentityUsers { get; set; }
    }
}
