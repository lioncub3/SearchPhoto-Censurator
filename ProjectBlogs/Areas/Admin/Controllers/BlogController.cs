using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch.Models;
using Newtonsoft.Json;
using ProjectBlogs.Data;
using ProjectBlogs.Models;

namespace ProjectBlogs.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private BlogsContext db;
        private SearchPhoto searchPhoto;

        public BlogController(BlogsContext db, UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.searchPhoto = new SearchPhoto("36a54313d1d94be381d680361638fdba");
        }
        public IActionResult Index()
        {
            return View(db.Blogs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            if (!ModelState.IsValid)
                return View(blog);

            blog.Author = User.Identity.Name;

            db.Blogs.Add(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
        {
            Blog blog = db.Blogs.First(i => i.Id == Id);

            return View(blog);
        }

        [HttpPost]
        public IActionResult Edit(Blog model)
        {
            if (!ModelState.IsValid)
                return View(model);

            db.Blogs.Update(model);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Blog blog = db.Blogs.First(i => i.Id == Id);

            db.Blogs.Remove(blog);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ViewComments(int Id)
        {
            return View(db.Comments.Where(c => c.Id_blog == Id));
        }

        public IActionResult DeleteComment(int Id)
        {
            Comment comment = db.Comments.First(i => i.Id == Id);

            db.Comments.Remove(comment);
            db.SaveChanges();

            return RedirectToAction("ViewComments");
        }

        [HttpPost]
        public IActionResult GetImages([FromBody]string searchTerm)
        {
            Images images = searchPhoto.GetPhotos(searchTerm);

            List<string> imageList = new List<string>();

            foreach (var img in images.Value.Take(4))
            {
                imageList.Add(img.ContentUrl);
            }

            var photos = JsonConvert.SerializeObject(imageList);
            return Json(photos);
        }

        [HttpPost]
        public IActionResult GetBadWords([FromBody]string text)
        {
            string startText = text.Replace(Environment.NewLine, " ");

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(text);
            MemoryStream stream = new MemoryStream(byteArray);

            bool is_badword = false;

            using (var client = Censurator.NewClient())
            {
                var screenResult = client.TextModeration.ScreenText("text/plain", stream, "eng", true, true, null, true);
                if (screenResult.Terms != null)
                    is_badword = true;
            }

            return Json(is_badword);
        }
    }
}