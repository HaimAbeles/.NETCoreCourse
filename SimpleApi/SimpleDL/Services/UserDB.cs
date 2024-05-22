using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleDB.EF.Contexts;
using SimpleDB.EF.Models;
using SimpleDB.Interfaces;
using SimpleEntites;

namespace SimpleDB.Services
{
    public class UserDB : IUserDB
    {
        private readonly SimpleContext _simpleContext;
        private readonly ILogger<UserDB> _logger;
        public UserDB(SimpleContext simpleContext, ILogger<UserDB> logger)
        {
            _simpleContext = simpleContext;
            _logger = logger;
        }

        public User GetUserById(string id)
        {
            return _simpleContext
                .Users
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == int.Parse(id));
        }

        public List<User> GetAllUsers()
        {
            return _simpleContext
                .Users
                .AsNoTracking()
                .ToList();
        }

        public void AddUserName(User user)
        {
            _simpleContext.Users.Add(user);
            _simpleContext.SaveChanges();
        }

        public List<User> GetUsersByClassId(string id)
        {
            return _simpleContext
                .Users
                .AsNoTracking()
                .Where(x => x.ClassId == int.Parse(id))
                .ToList();
        }

        public void UpdateUser(User user)
        {
            User userFromDb = _simpleContext
                .Users
                .FirstOrDefault(x => x.Id == user.Id);

            _logger.LogInformation("userFromClient: {@user}", user);

            userFromDb.UserName = user.UserName;
            _simpleContext.SaveChanges();
        }
    }
}
