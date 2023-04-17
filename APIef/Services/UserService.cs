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
            try
            {
                Role? role = _context.Roles.Find(1);
                if (role != null)
                {
                    user.Role = role;
                }

                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public User GetUser(string userName)
        {
            try
            {
                User? user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserName == userName);

                if (user != null)
                {

                    return user;
                }
                throw new ArgumentNullException();
            }
            catch
            {
                throw;
            }
        }

        public void DeleteUser(string userName)
        {
            try
            {
                User? user = _context.Users.Find(userName);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool UserExists(string userName)
        {
            try
            {
                return _context.Users.Any(u => u.UserName == userName);
            }
            catch
            {
                throw;
            }
        }

        public string CheckCredentials(UserCredentials userCredentials)
        {
            try
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
            catch
            {
                throw;
            }
        }


        public void ChangePassword(UserCredentials userCredentials)
        {
            try
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
            catch
            {
                throw;
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                return _context.Users.Include(u => u.Role).ToList();
            }
            catch
            {
                throw;
            }
        }
        public async Task AddUserAsync(User user)
        {
            try
            {
                Role? role = await _context.Roles.FindAsync(1);
                if (role != null)
                {
                    user.Role = role;
                }

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task AddAdmin(User user)
        {
            try
            {
                Role? role = await _context.Roles.FindAsync(3);
                if (role != null)
                {
                    user.Role = role;
                }

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<User> GetUserAsync(string userName)
        {
            try
            {
                User? user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserName == userName);

                if (user != null)
                {
                    return user;
                }
                throw new ArgumentNullException();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteUserAsync(string userName)
        {
            try
            {
                User? user = await _context.Users.FindAsync(userName);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
                else { throw new ArgumentNullException(); }
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<bool> UserExistsAsync(string userName)
        {
            try
            {
                return await _context.Users.AnyAsync(u => u.UserName == userName);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> CheckCredentialsAsync(UserCredentials userCredentials)
        {
            try
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
            catch
            {
                throw;
            }
        }

        public async Task ChangePasswordAsync(UserCredentials userCredentials)
        {
            try
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
            catch
            {
                throw;
            }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                return await _context.Users.Include(u => u.Role).ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }


}

