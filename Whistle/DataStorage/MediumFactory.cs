using DataStorage.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    public static class MediumFactory
    {
        public static IDatabase CreateDatabase(string dataSource)
        {
            return new SQLiteDB(dataSource);
        }
    }
}
