using DataStorage.Entity;
using Microsoft.Data.Entity;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.SQLite
{
    public class SQLiteDB : DbContext, IDatabase
    {
        private string DataSource { get; set; }
        // This property defines the table
        public DbSet<User> UserTable { get; set; }


        public SQLiteDB(string dataSource)
        {
            DataSource = dataSource;
        }

        public void Create()
        {
            if (!this.Database.EnsureCreated())
            {
                throw new InvalidOperationException("Database cannot be created!");
            }
        }

        // This method connects the context with the database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = this.DataSource };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }

        public IEnumerable<User> GetUsers()
        {
            return UserTable;
        }

        public void AddUser(User user)
        {
            UserTable.Add(user);
        }

        public void RemoveUser(User user)
        {
            UserTable.Remove(user);
        }

        public void Save()
        {
            this.SaveChanges();
        }
    }
}
