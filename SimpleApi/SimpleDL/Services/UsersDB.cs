using SimpleDB.EF.Contexts;
using SimpleDB.EF.Models;
using SimpleDB.Interfaces;
using SimpleEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDB.Services
{
    public class UsersDB : IUsersDB
    {
        private readonly SimpleContext _context;
        public UsersDB(SimpleContext context)
        {
            _context = context;
        }

        public User Login(User user)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
        }
    }
}
