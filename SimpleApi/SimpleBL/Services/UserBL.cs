using Microsoft.Extensions.Logging;
using SimpleBL.Interfaces;
using SimpleDB.EF.Models;
using SimpleDB.Interfaces;
using SimpleEntites;

namespace SimpleBL.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserDB _userDB;
        private readonly ILogger<UserBL> _logger;
        public UserBL(IUserDB userDB, ILogger<UserBL> logger)
        {
            _userDB = userDB;
            _logger = logger;
        }

        public User GetUserFirstNameByIdQuery(string id)
        {
            return _userDB.GetUserById(id);
        }

        public User GetUserByIdRoute(string idStr)
        {
            int id = 0;
            if(!int.TryParse(idStr, out id))
            {
                _logger.LogError("id is not int: {@id}", id);
            }
            return _userDB.GetUserById(idStr);
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
