using Microsoft.EntityFrameworkCore;
using SimpleDB.EF.Contexts;
using SimpleDB.EF.Models;
using SimpleDB.Interfaces;
using SimpleEntites;

namespace SimpleDB.Services
{
    public class UserDB : IUserDB
    {
        private readonly SimpleContext _simpleContext;
        public UserDB(SimpleContext simpleContext)
        {
            _simpleContext = simpleContext;
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

            userFromDb.UserName = user.UserName;
            _simpleContext.SaveChanges();
        }
    }
}
