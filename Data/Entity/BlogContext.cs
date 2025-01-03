using Microsoft.EntityFrameworkCore;

namespace efBlogApp.Data.Entity
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        // Fluent API configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User entity
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);  // Primary key configuration

            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade delete configuration

            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Post entity
            modelBuilder.Entity<Post>()
                .HasKey(p => p.PostId);  // Primary key configuration

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade delete configuration

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag",  // Junction table name
                    j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),  // Foreign key for Tag
                    j => j.HasOne<Post>().WithMany().HasForeignKey("PostId")  // Foreign key for Post
                );

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Commnets)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Comment entity
            modelBuilder.Entity<Comment>()
                .HasKey(c => c.CommentId); // Primary key configuration

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Commnets)
                .HasForeignKey(c => c.PostID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Tag entity
            modelBuilder.Entity<Tag>()
                .HasKey(t => t.TagId);  // Primary key configuration

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.Posts)
                .WithMany(p => p.Tags)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag",  // Junction table name
                    j => j.HasOne<Post>().WithMany().HasForeignKey("PostId"),  // Foreign key for Post
                    j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId")  // Foreign key for Tag
                );
        }
    }
}
