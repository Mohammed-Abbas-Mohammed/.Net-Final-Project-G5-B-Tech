using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ModelsB.Authentication_and_Authorization_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B
{
    public class UserService: IUserService
    {
       
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUserB> _userManager;

        public UserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUserB> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
      
        }

        public string GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId != null ? userId : "0"; 
        }
    }
}
