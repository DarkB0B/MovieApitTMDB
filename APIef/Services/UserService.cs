using APIef.Data;
using APIef.Interface;
using APIef.Models;
using Microsoft.EntityFrameworkCore;

namespace APIef.Services
{
    public class UserService : IUsers
    {
        readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        //implement IUsers interface with handling exceptions
        public void AddUser(User user)
        {

            Role? role = _context.Roles.Find(1);
            if (role != null)
            {
                user.Role = role;
            }

            _context.Users.Add(user);
            _context.SaveChanges();



        }

        public User GetUser(string userName)
        {

            User? user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserName == userName);

            if (user != null)
            {

                return user;
            }

            throw new ArgumentNullException();



        }

        public void DeleteUser(string userName)
        {

            User? user = _context.Users.Find(userName);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }





        }

        public void UpdateUser(User user)
        {

            _context.Users.Update(user);
            _context.SaveChanges();

        }

        public bool UserExists(string userName)
        {

            return _context.Users.Any(u => u.UserName == userName);

        }

        public string CheckCredentials(UserCredentials userCredentials)
        {

            User? user = _context.Users.Find(userCredentials.UserName);
            if (user != null)
            {
                if (user.Password == userCredentials.Password)
                {
                    return "OK";
                }
                else
                {
                    return "Wrong password";
                }
            }

            return "User Does Not Exist";

        }


        public void ChangePassword(UserCredentials userCredentials)
        {

            User? user = _context.Users.Find(userCredentials.UserName);
            if (user != null)
            {
                user.Password = userCredentials.Password;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("User Does Not Exist");
            }

        }

        public List<User> GetUsers()
        {

            return _context.Users.Include(u => u.Role).ToList();

        }

        public async Task AddUserAsync(User user)
        {

            Role? role = await _context.Roles.FindAsync(1);
            if (role != null)
            {
                user.Role = role;
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

        }

        public async Task AddAdmin(User user)
        {

            Role? role = await _context.Roles.FindAsync(3);
            if (role != null)
            {
                user.Role = role;
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

        }

        public async Task<User> GetUserAsync(string userName)
        {

            User? user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserName == userName);

            if (user != null)
            {
                return user;
            }

            throw new ArgumentNullException();

        }

        public async Task DeleteUserAsync(string userName)
        {

            User? user = await _context.Users.FindAsync(userName);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException();
            }

        }

        public async Task UpdateUserAsync(User user)
        {

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> UserExistsAsync(string userName)
        {

            return await _context.Users.AnyAsync(u => u.UserName == userName);

        }

        public async Task<string> CheckCredentialsAsync(UserCredentials userCredentials)
        {

            User? user = await _context.Users.FindAsync(userCredentials.UserName);
            if (user != null)
            {
                if (user.Password == userCredentials.Password)
                {
                    return "OK";
                }
                else
                {
                    return "Wrong password";
                }
            }

            return "User Does Not Exist";

        }

        public async Task ChangePasswordAsync(UserCredentials userCredentials)
        {

            User? user = await _context.Users.FindAsync(userCredentials.UserName);
            if (user != null)
            {
                user.Password = userCredentials.Password;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("User Does Not Exist");
            }


        }

        public async Task<List<User>> GetUsersAsync()
        {

            return await _context.Users.Include(u => u.Role).ToListAsync();

        }
    }
}




