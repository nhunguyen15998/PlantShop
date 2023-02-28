using PlantShop.Context;
using PlantShop.Models;

namespace PlantShop.Services 
{
    public interface IUserService
    {
        //get list
        public List<UserModel>? getUsers();
        //get user by id
        public UserModel? getUserById(int id);
        //get user by phone
        public UserModel? getUserByPhone(string phone);
        //create
        public int createUser(UserModel user);
        //update
        public bool updateUser(UserModel user);
        //delete
        public bool deleteUser(int id);
        //check unique phone 
        public bool checkUniqueUserPhone(string phone);
        //check valid login password
        public bool verifyPassword(string password);
    }

    public class UserService : IUserService
    {
        private PlantShopContext _context;

        public UserService(PlantShopContext context)
        {
            _context = context;
        }

        public bool checkUniqueUserPhone(string phone)
        {
            throw new NotImplementedException();
        }

        public int createUser(UserModel user)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.User.Add(user);
                _context.SaveChanges();

                transaction.Commit();
                return user.Id;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e);
                throw;
            }
        }

        public bool deleteUser(int id)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                UserModel? dbUser = _context.User.FirstOrDefault(x => x.Id == id);
                if(dbUser == null) return false;
                _context.User.Remove(dbUser);
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

        public UserModel? getUserById(int id)
        {
            try
            {
                return _context.User.FirstOrDefault(x => x.Id == id) ?? null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public UserModel? getUserByPhone(string phone)
        {
            try
            {
                return _context.User.FirstOrDefault(x => x.Phone == phone) ?? null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<UserModel>? getUsers()
        {
            try
            {
               return _context.User.ToList() ?? null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool updateUser(UserModel user)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                
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

        public bool verifyPassword(string password)
        {
            throw new NotImplementedException();
        }
    }
}