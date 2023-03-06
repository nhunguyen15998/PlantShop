using Microsoft.EntityFrameworkCore;
using PlantShop.Context;
using PlantShop.FormValidations;
using PlantShop.Models;

namespace PlantShop.Services 
{
    public interface IUserService
    {
        //get list
        public List<UserModel>? GetUsers();
        //get user by id
        // public UserModel? GetUserById(int id);
        //create
        public bool Register(RegisterForm user);
        //update
        public bool UpdateUser(UserModel user);
        //delete
        // public bool DeleteUser(int id);
        //login
        public UserModel? VerifyUser(SignInForm user);
        //check existing phone 
        public UserModel? CheckExistingUserPhone(string? phone);
        //check valid login password
        public bool VerifyPassword(string password, string? hashPassword);
    }

    public class UserService : IUserService
    {
        private PlantShopIdentityDbContext _context;

        public UserService(PlantShopIdentityDbContext context)
        {
            _context = context;
        }

        public UserModel? CheckExistingUserPhone(string? phone)
        {
            return _context.User.FirstOrDefault(x => x.Phone == phone) ?? null;
        }

        public bool Register(RegisterForm user)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                UserModel created = new UserModel();
                created.FirstName = user.FirstName;
                created.LastName = user.LastName;
                created.Email = user.Email;
                created.Phone = user.Phone;
                created.HashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _context.User.Add(created);
                _context.SaveChanges();

                transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e);
                throw;
            }
        }

        // public bool DeleteUser(int id)
        // {
        //     var transaction = _context.Database.BeginTransaction();
        //     try
        //     {
        //         UserModel? existingUser = _context.User.FirstOrDefault(x => x.Id == id);
        //         if(existingUser == null) return false;
        //         _context.User.Remove(existingUser);
        //         _context.SaveChanges();

        //         transaction.Commit();
        //         return true;
        //     }
        //     catch (Exception e)
        //     {
        //         transaction.Rollback();
        //         Console.WriteLine(e);
        //         throw;
        //     }
        // }

        // public UserModel? GetUserById(int id)
        // {
        //     return _context.User.FirstOrDefault(x => x.Id == id) ?? null;
        // }

        public List<UserModel>? GetUsers()
        {
            return _context.User.ToList() ?? null;
        }

        public UserModel? VerifyUser(SignInForm user)
        {
            try
            {
                //check phone
                UserModel? existingUser = this.CheckExistingUserPhone(user.Phone);
                if(existingUser == null) return null;
                //check password
                bool isValidPassword = this.VerifyPassword(user.Password, existingUser.HashPassword);
                if(!isValidPassword) return null;
                return existingUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool UpdateUser(UserModel user)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                UserModel? existingUser = _context.User.FirstOrDefault(x => x.Id == user.Id);
                if(existingUser == null) return false;
                _context.Entry<UserModel>(existingUser).CurrentValues.SetValues(user); 
                _context.Entry<UserModel>(existingUser).State = EntityState.Modified;
                _context.SaveChanges();

                transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e);
                throw;
            }
        }

        public bool VerifyPassword(string password, string? hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
    }
}