using SimpleEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDB.Interfaces
{
    public interface IUserDB
    {
        void AddUserName(User user);
    }
}
