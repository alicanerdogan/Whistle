using DataStorage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    public interface IDatabase : IDisposable
    {
        void Create();
        void Save();
        IEnumerable<User> GetUsers();
        void AddUser(User user);
        void RemoveUser(User user);
    }
}
