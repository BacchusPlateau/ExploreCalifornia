using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCalifornia1.Data;
using ExploreCalifornia1.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExploreCalifornia1.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        public BlogDataContext Db { get; }

        public BlogController(BlogDataContext db)
        {
            Db = db;
        }

        [Route("")]
        public IActionResult Index()
        {
            var posts = Db.Posts
                .OrderByDescending(x => x.Posted)
                .Take(2)
                .ToList();

            return View(posts);
        }

        [Route("{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            var post = Db.Posts
                .Where(x => x.Key == key)
                .Single();
            
            return View(post);
        }

        [Route("create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
                return View();

            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;

            Db.Posts.Add(post);
            Db.SaveChanges();

            return RedirectToAction("Post", "Blog", new
            {
                year = post.Posted.Year,
                month = post.Posted.Month,
                key = post.Key
            });
        }
    }
}
