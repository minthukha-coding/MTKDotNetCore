using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.RestAPIWithNLayer.Database;

namespace MTKDotNetCore.RestAPIWithNLayer.Features.BLOG
{
    public class DA_Blog
    {
        private readonly AppDbContext _appDbContext;
        public DA_Blog()
        {
            _appDbContext = new AppDbContext();
        }
        public List<BlogModel> GetBlogs()
        {
            var lst = _appDbContext.Blogs.ToList();
            return lst;
        }    
        public BlogModel GetBlog(int id)
        {
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            return item;
        }
        public int Createblog(BlogModel reqModel)
        {
            _appDbContext.Blogs.Add(reqModel);
            var result = _appDbContext.SaveChanges();
            return result;
        }
        public int Updateblog(int id ,BlogModel reqModel)
        {
            var item = _appDbContext.Blogs.FirstOrDefault( x => x.BlogId == id);
            if(item == null)
            {
                return 0;
            }

            item.BlogId = id;
            item.BlogTitle = reqModel.BlogTitle;
            item.BlogAuthor = reqModel.BlogAuthor;
            item.BlogContent = reqModel.BlogContent;

            var result = _appDbContext.SaveChanges();
            return result;
        }
        public int Deleteblog(int id)
        {
            var item = _appDbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return 0;
            }
            _appDbContext.Blogs.Remove(item);
            var result = _appDbContext.SaveChanges();
            return result; 
        }
        public int PatchBlog(int id, BlogModel requestModel)
        {
            var item = _appDbContext.Blogs.First(x => x.BlogId == id);
            if (item is null)
                return 0;

            if (!string.IsNullOrEmpty(requestModel.BlogTitle))
            {
                item.BlogTitle = requestModel.BlogTitle;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
            {
                item.BlogAuthor = requestModel.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogContent))
            {
                item.BlogContent = requestModel.BlogContent;
            }

            int result = _appDbContext.SaveChanges();
            return result;
        }
    }
}
