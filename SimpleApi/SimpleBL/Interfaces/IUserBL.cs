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
        string GetUserFirstName();
        string GetUserFirstNameByIdQuery(string id);
        string GetUserFirstNameByIdRoute(string id);
        List<string> GetAllUserNames();
        void AddUserName(User user);
        User Test();
    }
}
