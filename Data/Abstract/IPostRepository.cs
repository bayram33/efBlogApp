using efBlogApp.Data.Entity;

namespace efBlogApp.Data.Abstract
{
    public interface IPostRepository
    {
        List<Post> Posts { get; set; }
        //void CreatePost(Post post);
    }
}
