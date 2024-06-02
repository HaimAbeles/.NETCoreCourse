using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using SimpleBL.Interfaces;
using SimpleEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBL.Services
{
    public class IndexBL : IIndexBL
    {
        private readonly AppSettings _appSettings;
        public IndexBL(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }
        public int GetIndex()
        {

            return 10;
        }

        public int GetMinPrice()
        {
            return _appSettings.Price.min;
        }
    }
}
