using SimpleEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBL.Interfaces
{
    public interface IRestApiGW
    {
        T ApiRequest<T>(ApiRequestModel apiRequestModel);
    }
}
