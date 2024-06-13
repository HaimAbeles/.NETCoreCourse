using Microsoft.EntityFrameworkCore;
using SimpleDB.EF.Contexts;
using SimpleDB.EF.Models;
using SimpleDB.Interfaces;
using SimpleEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public void SaveDate()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    DateOnly date = DateOnly.MaxValue; //get from table dates - 09.06.24
                    _context.SaveChanges();
                    TimeOnly time = TimeOnly.MaxValue; //get from table times - 20:00
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
