using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Data.Repositories;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Manager;
using ASI.Basecode.Services.ServiceModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static ASI.Basecode.Resources.Constants.Enums;

namespace ASI.Basecode.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public LoginResult AuthenticateUser(string userId, string password, ref User user)
        {
            user = new User();
            var passwordKey = PasswordManager.EncryptPassword(password);
            user = _repository.GetUsers().Where(x => x.UserId == userId &&
                                                     x.Password == passwordKey).FirstOrDefault();

            return user != null ? LoginResult.Success : LoginResult.Failed;
        }

        public void AddUser(UserViewModel model)
        {
            var user = new User();
            if (!_repository.UserExists(model.UserId))
            {
                _mapper.Map(model, user);
                //user.Password = PasswordManager.EncryptPassword(model.Password);
                user.CreatedTime = DateTime.Now;
                user.UpdatedTime = DateTime.Now;
                user.CreatedBy = System.Environment.UserName;
                user.UpdatedBy = System.Environment.UserName;

                _repository.AddUser(user);
            }
            else
            {
                throw new InvalidDataException(Resources.Messages.Errors.UserExists);
            }
        }

        public Task<IdentityUser> FindUserAsync(string userName, string password)
        {
            return _repository.FindUserAsync(userName, password);
        }

        public IdentityUser FindUser(string userName)
        {
            return _repository.FindUser(userName);
        }

        public async Task<IdentityResult> CreateRole(string roleName)
        {
            return await _repository.CreateRole(roleName);
        }

        public IQueryable<IdentityRole> GetRoles()
        {
            return _repository.GetRoles();
        }

        public IQueryable<User> GetUsers()
        {
            return _repository.GetUsers();
        }

        public async Task UpdateUser(UserViewModel model)
        {
            var user = new User();
            var originalEmail = model.OriginalEmail;
            var role = model.Role;
            if (_repository.UserExists(model.UserId))
            {
                _mapper.Map(model, user);
                user.UpdatedTime = DateTime.Now;
                user.UpdatedBy = System.Environment.UserName;
                _repository.UpdateUser(user);
				Task.Run(async () => await _repository.UpdateIdentityUser(user, originalEmail, role)).Wait();
			}

        }
        public async Task DeleteUser(string userId)
        {
            if (_repository.UserExists(userId))
            {
                await _repository.DeleteUser(userId);
            }
        }
    }
}
