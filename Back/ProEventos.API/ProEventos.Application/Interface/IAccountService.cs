using Microsoft.AspNetCore.Identity;
using ProEventos.Application.Dto;
using System.Threading.Tasks;

namespace ProEventos.Application.Interface {
    public interface IAccountService {
        Task<bool> UserExists(string username);
        Task<UserUpdateDto> GetUserByUsernameAsync(string username);

        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string passwaord);

        Task<UserDto> CreateAccountAsync(UserDto userDto);

        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);




    }
}
