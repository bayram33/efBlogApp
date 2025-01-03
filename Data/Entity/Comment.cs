﻿namespace efBlogApp.Data.Entity
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? Text { get; set; }
        public DateTime PublishedOn { get; set; }

        public int PostID { get; set; }
        public Post Post { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
