using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application.Dto;
using ProEventos.Application.Interface;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers {

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase {

        private readonly ITokenService _tokenService;
        public readonly IAccountService _accountService;

        public AccountController(IAccountService accountService, ITokenService tokenService) {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser() {
            try {
                var username = User.GetUsername();
                var user = await _accountService.GetUserByUsernameAsync(username);
                return Ok(user);

            } catch (Exception ex) {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar recuperar usuário, Erro: " + ex.Message);
            }
        }

        [HttpPost("RegisterUser")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(UserDto userDto) {
            try {
                if (await _accountService.UserExists(userDto.Username)) {
                    return BadRequest("Usuário já existe");
                }

                var user = await _accountService.CreateAccountAsync(userDto);

                if (user != null) {
                    return Ok(user);
                }
                return BadRequest("Usuário não criado, tente novamente mais tarde!");

            } catch (Exception ex) {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar criar usuário, Erro: " + ex.Message);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto) {
            try {

                var user = await _accountService.GetUserByUsernameAsync(userLoginDto.Username);
                if (user == null) { return Unauthorized("Usuário inválido!"); }

                var result = await _accountService.CheckUserPasswordAsync(user, userLoginDto.Password);
                if (!result.Succeeded) { return Unauthorized("Usuário inválido!"); }

                return Ok(new {
                    Username =  user.Username,
                    PrimeiroNome = user.PrimeiroNome,
                    Token = _tokenService.CreateToken(user).Result
                });

            } catch (Exception ex) {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar logar, Erro: " + ex.Message);
            }
        }


        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto) {
            try {
                var user = await _accountService.GetUserByUsernameAsync(User.GetUsername());
                if (user == null) { return Unauthorized("Usuário inválido!"); }

                var userRreturn = await _accountService.UpdateAccount(userUpdateDto);

                if (userRreturn == null) {
                    return NoContent();
                }
                return Ok(userRreturn);

            } catch (Exception ex) {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar atualizar usuário, Erro: " + ex.Message);
            }
        }

    }
}
