using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStorage.SQLite;
using DataStorage;

namespace DataStorageTest
{
    [TestClass]
    public class SQLiteTest
    {
        [TestMethod]
        public void CreateDB()
        {
            if (System.IO.File.Exists("test.sqlite"))
            {
                System.IO.File.Delete("test.sqlite");
            }
            using (var db = MediumFactory.CreateDatabase("test.sqlite"))
            {
                db.Create();
            }
            if (!System.IO.File.Exists("test.sqlite"))
            {
                Assert.Fail("Database file is not created.");
            }
        }

        [TestMethod]
        public void AddUserToDB()
        {
            using (var db = MediumFactory.CreateDatabase("test.sqlite"))
            {
                var users = db.GetUsers();
                db.AddUser(new DataStorage.Entity.User() { Username = "aeality" });
                db.Save();
            }
        }
    }
}
