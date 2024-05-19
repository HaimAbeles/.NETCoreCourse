using SimpleDB.EF.Models;
using SimpleEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBL.Interfaces
{
    public interface IUserBL
    {
        User GetUserFirstNameByIdQuery(string id);
        User GetUserByIdRoute(string id);
        List<User> GetAllUsers();
        void AddUserName(User user);
        List<User> GetUsersByClassId(string id);
        void UpdateUser(User user);
    }
}
