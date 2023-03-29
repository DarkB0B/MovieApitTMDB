using APIef.Models;

namespace APIef.Interface
{
    public interface IUsers
    {
        public void AddUser(User user);
        public User GetUser(string userName);
        public void UpdateUser(User user);
        public void DeleteUser(string userName);
        public bool UserExists(string userName);
        public string CheckCredentials(UserCredentials userCredentials);
        public void ChangePassword(UserCredentials userCredentials);
        public List<User> GetUsers();
    }
}
