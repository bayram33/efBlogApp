using efBlogApp.Data.Abstract;
using efBlogApp.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace efBlogApp.Controllers
{
    public class PostController(IPostRepository repo) : Controller
    {
        public IActionResult Index()
        {
            var result =repo.Posts;
            return View(result);
        }

    }
}
