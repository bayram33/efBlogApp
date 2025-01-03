using efBlogApp.Data.Abstract;
using efBlogApp.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace efBlogApp.Data.Concrete
{
    public class PostRepository(BlogContext _context) : IPostRepository
    {
        public List<Post> Posts
        {
            get { return _context.Posts.ToList(); }
            set { }
        }
    }
}
