using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.ConsoleApp.RefitExample
{
    public interface IBlogApi
    {
        [Get("api/blogEfCore")]
        Task<List<BlogModel>> GetBlogs();

        [Get("api/blogEfCore/{id}")]
        Task<BlogModel> GetBlog(int blogId);

        [Post("api/blogEfcore")]
        Task<BlogModel> CreateBlog(BlogModel blog);

        [Put("api/blogEfCore/{id}")]
        Task<BlogModel> UpdateBlog(int id ,BlogModel blog);

        [Delete("{id}")]
        Task<BlogModel> DeleteBlog(int id);
    }
    public class BlogModel
    {
        public int BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogAuthor { get; set; }
        public string? BlogContent { get; set; }
    }
}
