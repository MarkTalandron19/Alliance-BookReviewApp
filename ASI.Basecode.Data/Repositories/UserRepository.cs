using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ASI.Basecode.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserRepository(IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager) : base(unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IQueryable<User> GetUsers()
        {
            return this.GetDbSet<User>();
        }

        public bool UserExists(string userId)
        {
            return this.GetDbSet<User>().Any(x => x.UserId == userId);
        }

        public void AddUser(User user)
        {
            this.GetDbSet<User>().Add(user);
            UnitOfWork.SaveChanges();
        }
        public IdentityUser FindUser(string userName)
        {
            var userDB = GetDbSet<IdentityUser>().Where(x => x.UserName.ToLower().Equals(userName.ToLower())).AsNoTracking().FirstOrDefault();
            return userDB;
        }

        public async Task<IdentityUser> FindUserAsync(string userName, string password)
        {
            var userDB = GetDbSet<IdentityUser>().Where(x => x.UserName.ToLower().Equals(userName.ToLower())).AsNoTracking().FirstOrDefault();
            var user = await _userManager.FindByNameAsync(userName);
            var isPasswordOK = await _userManager.CheckPasswordAsync(user, password);
            if ((user == null) || (isPasswordOK == false))
            {
                userDB = null;
            }
            return userDB;
        }

        public async Task<IdentityResult> CreateRole(string roleName)
        {
            bool checkIfRoleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!checkIfRoleExists)
            {
                var role = new IdentityRole();
                role.Name = roleName;
                var result = await _roleManager.CreateAsync(role);
                return result;
            }

            return null;
        }

        public IQueryable<IdentityRole> GetRoles()
        {
            return this.GetDbSet<IdentityRole>();
        }

        public void UpdateUser(User model)
        {
            var user = this.GetDbSet<User>().SingleOrDefault(u => u.UserId == model.UserId);
            if (user != null)
            {
                user.Name = model.Name;
                user.UserId = model.UserId;
                user.Email = model.Email;
                user.Password = model.Password;
                user.UpdatedBy = model.UpdatedBy;
                user.UpdatedTime = model.UpdatedTime;
                UnitOfWork.SaveChanges();
            }
        }

        public async Task UpdateIdentityUser(User model, string originalEmail, string role)
        {
            var user = await _userManager.FindByEmailAsync(originalEmail);
            var identityUser = new IdentityUser();
            if (user != null)
            {
                var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                user.UserName = model.Name;
                user.Email = model.Email;
                user.PasswordHash = newPasswordHash;
                await _userManager.UpdateAsync(user);
                await _userManager.UpdateNormalizedEmailAsync(user);
                await _userManager.UpdateNormalizedUserNameAsync(user);

                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRoleAsync(user, role);
            }
        }

        public async Task DeleteUser(string userId)
        {
            var user = this.GetDbSet<User>().SingleOrDefault(u => u.UserId == userId);
            var email = user.Email;

            if (user != null)
            {
                this.GetDbSet<User>().Remove(user);
                Task.Run(async () => await DeleteIdentityUser(email)).Wait();
                UnitOfWork.SaveChanges();
            }
        }

        public async Task DeleteIdentityUser(string originalEmail)
        {
            var user = await _userManager.FindByEmailAsync(originalEmail);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                var result = await _userManager.DeleteAsync(user);

            }
        }
        public async Task<User> GetUserById(string userId)
        {
            var user = await this.GetDbSet<User>().Where(u => u.UserId == userId).SingleOrDefaultAsync();

            return user;
        }
    }
}
