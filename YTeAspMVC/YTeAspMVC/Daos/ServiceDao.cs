using YTeAspMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YTeAspMVC.Daos
{
    public class ServiceDao
    {
        YTeDBContext myDb = new YTeDBContext();

        public List<Service> GetServices()
        {
            return myDb.Services.ToList();
        }

        public Service GetService(int id)
        {
            return myDb.Services.FirstOrDefault(x => x.IdService == id);
        }

        public List<Service> GetTop3()
        {
            return myDb.Services.OrderByDescending(x => x.IdService).Take(3).ToList();
        }

        public List<Service> GetService(int page, int pagesize)
        {
            var arrIdInsurances = myDb.Services.Where(x => x.Status == 1).Select(x => x.IdService).ToList();
            return myDb.Services.ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }
        
        public List<Service> GetAll()
        {
            return myDb.Services.ToList();
        }

        public void Add(Service service)
        {
            myDb.Services.Add(service);
            myDb.SaveChanges();
        }

        public void Update(Service service)
        {
            var obj = myDb.Services.FirstOrDefault(x => x.IdService == service.IdService);
            obj.Name = service.Name;
            obj.Content = service.Content;
            obj.Image = service.Image;
            obj.Price = service.Price;
            myDb.SaveChanges();
        }
        public void Delete(int id)
        {
            var obj = myDb.Services.FirstOrDefault(x => x.IdService == id);
            myDb.Services.Remove(obj);
            myDb.SaveChanges();
        }
    }
}