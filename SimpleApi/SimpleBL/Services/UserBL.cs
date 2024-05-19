using SimpleBL.Interfaces;
using SimpleDB.EF.Models;
using SimpleDB.Interfaces;
using SimpleEntites;

namespace SimpleBL.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserDB _userDB;
        public UserBL(IUserDB userDB)
        {
            _userDB = userDB;
        }

        public User GetUserFirstNameByIdQuery(string id)
        {
            return _userDB.GetUserById(id);
        }

        public User GetUserByIdRoute(string id)
        {
            return _userDB.GetUserById(id);
        }

        public List<User> GetAllUsers()
        {
            return _userDB.GetAllUsers();
        }

        public void AddUserName(User user)
        {
            _userDB.AddUserName(user);
        }

        public List<User> GetUsersByClassId(string id) 
        {
            return _userDB.GetUsersByClassId(id);
        }

        public void UpdateUser(User user)
        {
            _userDB.UpdateUser(user);
        }
    }
}
