using APIef.Data;
using APIef.Interface;
using APIef.Models;

namespace APIef.Repository
{
    public class UserRepository : IUsers
    {
        readonly DataContext _context;
        public UserRepository(DataContext context)
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
                User? user = _context.Users.Find(userName);
                if (user != null)
                {
                    Role? role = _context.Roles.Find(user.RoleId);
                    if (role != null)
                    {
                        user.Role = role;
                    }
                    else
                    {
                        throw new ArgumentNullException("Role is null");
                    }
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
                throw new ArgumentNullException();
            }
            catch
            {
                throw;
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
        

    }
}
