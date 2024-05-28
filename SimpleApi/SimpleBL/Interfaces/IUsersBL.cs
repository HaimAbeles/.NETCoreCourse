using SimpleDB.EF.Models;
using SimpleEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBL.Interfaces
{
    public interface IUsersBL
    {
        bool Login(UserLogin user);
    }
}
