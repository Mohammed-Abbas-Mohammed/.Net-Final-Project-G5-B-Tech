using DTOsB.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAppUsersAsync();
        Task<UserDto> GetAppUserByIdAsync(string id);
        Task<UserDto> GetAppUserByEmailAsync(string email);
        Task UpdateAppUserAsync(UserDto userDto);
        Task DeleteAppUserAsync(string id);
    }
}
