using YTeAspMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace YTeAspMVC.Daos
{
    public class NumberDao
    {
        YTeDBContext myDb = new YTeDBContext();
        public List<Number> GetNumberByUser(int id)
        {
            return myDb.Numbers.Where(x => x.IdUser == id).OrderByDescending(x => x.IdNumber).ToList();
        }



        public Number GetById(int id)
        {
            return myDb.Numbers.FirstOrDefault(x => x.IdNumber == id);
        }



        public void Add(Number number)
        {
            myDb.Numbers.Add(number);
            myDb.SaveChanges();
        }
        public void Remove(int numberId)
        {
            var number = myDb.Numbers.FirstOrDefault(x => x.IdNumber == numberId);
            myDb.Numbers.Remove(number);
            myDb.SaveChanges();
        }


        public int GetNumberToday()
        {
            string date = DateTime.Now.Date.ToString();
            var numberOfDay = myDb.Numbers.Where(x => x.Day.Equals(date)).OrderByDescending(x => x.NumberInt).ToList();
            if (numberOfDay.Count == 0)
            {
                return 0;
            }
            else
            {
                return numberOfDay.FirstOrDefault().NumberInt;
            }



        }

        public bool CheckUserNumberDay(int userId)
        {
            string date = DateTime.Now.Date.ToString();
            var numberOfDay = myDb.Numbers.FirstOrDefault(x => x.Day.Equals(date) && x.IdUser == userId);
            if (numberOfDay == null)
            {
                return true;
            }
            return false;
        }

        public List<Number> GetNumber()
        {
            return myDb.Numbers.ToList();
        }
    }
}