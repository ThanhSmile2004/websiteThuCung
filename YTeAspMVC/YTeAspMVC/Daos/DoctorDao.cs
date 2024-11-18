using YTeAspMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YTeAspMVC.Daos
{
    public class DoctorDao
    {
        YTeDBContext myDb = new YTeDBContext();

        public List<Doctor> GetAll()
        {
            return myDb.Doctors.ToList();
        }

        public List<Doctor> Search(string keyword)
        {
            return myDb.Doctors.Where(x => x.FullName.Contains(keyword)).ToList();
        }

        public void Add(Doctor doctor)
        {
            myDb.Doctors.Add(doctor);
            myDb.SaveChanges();
        }

        public void Update(Doctor doctor)
        {
            var obj = myDb.Doctors.FirstOrDefault(x => x.IdDoctor == doctor.IdDoctor);
            obj.Email = doctor.Email;
            obj.FullName = doctor.FullName;
            obj.Password = doctor.Password;
            obj.Specialist = doctor.Specialist;
            obj.Describe = doctor.Describe;
            obj.Image = doctor.Image;
            myDb.SaveChanges();
        }
        public void Delete(int id)
        {
            var obj = myDb.Doctors.FirstOrDefault(x => x.IdDoctor == id);
            myDb.Doctors.Remove(obj);
            myDb.SaveChanges();
        }

        public bool checkLogin(string email, string password)
        {
            var obj = myDb.Doctors.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (obj == null) { return false; }
            return true;
        }

        public Doctor getUserByEmail(string email)
        {
            return myDb.Doctors.FirstOrDefault(x => x.Email.Equals(email));
        }

        public Doctor getDoctor(int id)
        {
            return myDb.Doctors.FirstOrDefault(x => x.IdDoctor == id);
        }

        public List<Doctor> GetTop3()
        {
            return myDb.Doctors.OrderByDescending(x => x.IdDoctor).Take(3).ToList();
        }
    }
}