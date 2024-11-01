using ApplicationB.Contracts_B.User;
using AutoMapper;
using DTOsB.Account;
using DTOsB.User;
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

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper , IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId != null ? userId : "0";
        }

        public async Task<IEnumerable<UserDto>> GetAllAppUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetAppUserByIdAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetAppUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateAppUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<ApplicationUserB>(userDto);
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteAppUserAsync(string id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        public async Task<IdentityResult> CreateAppUserAsync(UserDto UserDto) // تنفيذ جديد
        {
            var user = _mapper.Map<ApplicationUserB>(UserDto);
            return await _userRepository.CreateUserAsync(user, UserDto.Password);
        }

    }
}
