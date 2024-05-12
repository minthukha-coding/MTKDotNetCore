namespace MTKDotNetCore.RestAPIWithNLayer.Features.BLOG
{
    public class BL_Blog
    {
        private readonly DA_Blog _daBlog;

        public BL_Blog(DA_Blog daBlog)
        {
            _daBlog = daBlog;
        }
        public List<BlogModel> GetBlogs()
        {
            var lst = _daBlog.GetBlogs();
            return lst;
        }
        public BlogModel GetBlog(int id)
        {
            var item = _daBlog.GetBlog(id);
            return item;
        }
        public int CreateBlog(BlogModel requestModel)
        {
            var result = _daBlog.Createblog(requestModel);
            return result;
        }

        public int UpdateBlog(int id, BlogModel requestModel)
        {
            var result = _daBlog.Updateblog(id, requestModel);
            return result;
        }

        public int PatchBlog(int id, BlogModel requestModel)
        {
            var result = _daBlog.PatchBlog(id, requestModel);
            return result;
        }
        public int DeleteBlog(int id)
        {
            var result = _daBlog.Deleteblog(id);
            return result;
        }
    }
}
