using BlogMvcApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlogMvcApp.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
