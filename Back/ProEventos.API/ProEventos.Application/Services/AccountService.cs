using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Dto;
using ProEventos.Application.Interface;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Services {
    public class AccountService : IAccountService {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserPersistence _userPersistence;


        public AccountService(UserManager<User> userManger, SignInManager<User> signInManager,
                               IMapper mapper, IUserPersistence userPersistence) {
            _userManager = userManger;
            _signInManager = signInManager;
            _mapper = mapper;
            _userPersistence = userPersistence;



        }
        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password) {
            try {
                var user = await _userManager.Users
                    .SingleOrDefaultAsync(user => user.UserName.ToLower() == userUpdateDto.Username.ToLower());

                //se passar true  e o user e password  fr incorreto irá bloquear o usuário
                return await _signInManager.CheckPasswordSignInAsync(user, password, false);

            } catch (Exception ex) {

                throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
            }
        }

        public async Task<UserDto> CreateAccountAsync(UserDto userDto) {
            try {
                var user = _mapper.Map<User>(userDto);
                var result =  await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded) {
                    var userToReturn = _mapper.Map<UserDto>(user);
                    return userToReturn;
                }
                return null;

            } catch (Exception ex) {

                throw new Exception($"Erro ao tentar criar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUsernameAsync(string username) {
            try {
                var user = await _userPersistence.GetUserByUsernameAsync(username);
                if (user == null) {
                    return null;
                }
                var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
                return userUpdateDto;
            } catch (Exception ex) {

                throw new Exception($"Erro ao tentar buscar usuário pelo nome. Erro: {ex.Message}");
            }
        }


        public async Task<bool> UserExists(string username) {
            try {
                return await _userManager.Users
                    .AnyAsync(user => user.UserName.ToLower() == username.ToLower());
            } catch (Exception ex) {

                throw new Exception($"Erro ao verificar se o usuário existe. Erro: {ex.Message}");
            }
        }
            public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto) {
            try {
                    var user = await _userPersistence.GetUserByUsernameAsync(userUpdateDto.Username);
                    if (user == null) {
                        return null;
                    }
                    _mapper.Map(userUpdateDto, user);
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                    _userPersistence.Update<User>(user);

                    if (await _userPersistence.SaveChangesAsync()) {
                        var useRetorno = await _userPersistence.GetUserByUsernameAsync(user.UserName);

                        return _mapper.Map<UserUpdateDto>(useRetorno);
                    }

                    return null;
            } catch (Exception ex) {

                throw new Exception($"Erro ao atualizar usuário. Erro: {ex.Message}");
            }
        }


        }
    }

