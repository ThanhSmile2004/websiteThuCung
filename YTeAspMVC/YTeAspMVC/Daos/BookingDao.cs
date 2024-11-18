using YTeAspMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace YTeAspMVC.Daos
{
    public class BookingDao
    {
        YTeDBContext myDb = new YTeDBContext();

        public void Add(Booking booking)
        {
            myDb.Bookings.Add(booking);
            myDb.SaveChanges();
        }

        public void delete(int idBooking)
        {
            var booking = myDb.Bookings.FirstOrDefault(x => x.IdBooking == idBooking);
            myDb.Bookings.Remove(booking);
            myDb.SaveChanges();
        }

        public List<Booking> GetBookingByUser(int idUser)
        {
            return myDb.Bookings.Where(x => x.IdUser == idUser).ToList();
        }
       
        public List<Booking> GetBookingByDoctor(int idDoctor)
        {
            return myDb.Bookings.Where(x => x.IdDoctor == idDoctor && x.Status == 1).ToList();
        }


        public List<Booking> GetAll()
        {
            return myDb.Bookings.Where(x => x.Status != 2).ToList();
        }

        public List<Booking> GetHistory()
        {
            return myDb.Bookings.Where(x => x.Status == 2).ToList();
        }

        public Booking GetDetail(int id)
        {
            return myDb.Bookings.FirstOrDefault(x => x.IdBooking == id);
        }

        public void Approve(int id)
        {
            var obj = myDb.Bookings.FirstOrDefault(x => x.IdBooking == id);
            obj.Status = 1;
            myDb.SaveChanges();
        }

        public void Complete(int id)
        {
            var obj = myDb.Bookings.FirstOrDefault(x => x.IdBooking == id);
            obj.Status = 2;
            myDb.SaveChanges();
        }

        public int CheckNumberInDay(string day)
        {
            return myDb.Bookings.Where(x => x.Day.Equals(day)).ToList().Count;
        }



        public List<int> GetHourInDay(string day, int currentHour)
        {
            List<int> arrHours = new List<int>();
            for (int i = currentHour + 1; i < 18; i++)
            {
                arrHours.Add(i);
            }
            var bookings  = myDb.Bookings.Where(x => x.Day.Equals(day)).ToList();
            for(int i = 0; i < bookings.Count; i++)
            {
                for (int j = 0; j < arrHours.Count; j++)
                {             
                    if (bookings[i].Time == arrHours[j])
                    {
                        arrHours.RemoveAt(j);
                    }
                }
            }
            return arrHours;
        }



        public bool CheckExistScheduleInDay(string day, int hour, int userId,int doctorId)
        {
            var bookings = myDb.Bookings.Where(x => x.Day.Equals(day) && x.Time == hour && x.IdUser == userId && x.IdDoctor == doctorId).ToList();
            if (bookings.Count < 6)
            {
                return true;
            }

            return false;
        }
        public Booking GetBookingById(int id)
        {
            using (var context = new YTeDBContext())
            {
                return context.Bookings.FirstOrDefault(b => b.IdBooking == id);
            }
        }
        public void UpdateBooking(Booking booking)
        {
            using (var context = new YTeDBContext())
            {
                context.Entry(booking).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}