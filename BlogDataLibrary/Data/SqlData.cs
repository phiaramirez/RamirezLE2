using BlogDataLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDataLibrary.Data
{
    public class SqlData
    {
        private ISqlDataAccess _db;
        private const string connectionStringName = "SqlDB";
        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }
    }
}
