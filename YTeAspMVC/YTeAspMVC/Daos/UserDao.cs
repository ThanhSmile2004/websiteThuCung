using YTeAspMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;



namespace YTeAspMVC.Daos
{
    public class UserDao
    {
        YTeDBContext myDb = new YTeDBContext();
        public bool checkLogin(string email, string password)
        {
            var obj = myDb.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (obj == null) { return false; }
            return true;
        }

        public User getUserByEmail(string email)
        {
            return myDb.Users.FirstOrDefault(x => x.Email.Equals(email));
        }


        public List<User> getUser()
        {
            return myDb.Users.Where(x => x.IdRole == 1).ToList();
        }

        public void Add(User user)
        {
            myDb.Users.Add(user);
            myDb.SaveChanges();
        }
        public User getById(int id)
        {
            return myDb.Users.FirstOrDefault(x => x.IdUser == id);
        }
        public void Update(User user)
        {
            var obj = myDb.Users.FirstOrDefault(x => x.IdUser == user.IdUser);
            obj.Email = user.Email;
            obj.FullName = user.FullName;
            obj.Password = user.Password;
            obj.PhoneNumber = user.PhoneNumber;
            obj.Address = user.Address;
            obj.Gender = user.Gender;
            myDb.SaveChanges();
        }
        public void Delete(int id)
        {
            var obj = myDb.Users.FirstOrDefault(x => x.IdUser == id);
            myDb.Users.Remove(obj);
            myDb.SaveChanges();
        }

        public bool checkExistEmail(string email)
        {
            var user = myDb.Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}