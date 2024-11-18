using YTeAspMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YTeAspMVC.Daos
{
    public class PostDao
    {
        YTeDBContext myDb = new YTeDBContext();

        public List<Post> GetAll()
        {
            return myDb.Posts.ToList();
        }

        public Post GetPost(int id)
        {
            return myDb.Posts.FirstOrDefault(x => x.IdPost == id);
        }

        public void Add(Post post)
        {
            myDb.Posts.Add(post);
            myDb.SaveChanges();
        }

        public void AddAdvise(Advise advise)
        {
            myDb.Advises.Add(advise);
            myDb.SaveChanges();
        }

        public List<Advise> GetAdvise()
        {
            return myDb.Advises.OrderByDescending(x => x.IdAdvise).ToList();
        }

        public Advise GetAdviseDetail(int id)
        {
            return myDb.Advises.FirstOrDefault(x => x.IdAdvise == id);
        }

        public void Update(Post post)
        {
            var obj = myDb.Posts.FirstOrDefault(x => x.IdPost == post.IdPost);
            obj.Title = post.Title;
            obj.Description = post.Description;
            obj.Image = post.Image;
            myDb.SaveChanges();
        }
        public void Delete(int id)
        {
            var obj = myDb.Posts.FirstOrDefault(x => x.IdPost == id);
            myDb.Posts.Remove(obj);
            myDb.SaveChanges();
        }

        public List<Post> GetTop3()
        {
            return myDb.Posts.OrderByDescending(x => x.IdPost).Take(3).ToList();
        }
    }
}