using Microsoft.AspNetCore.Mvc;

namespace MTKDotNetCore.RestAPIWithNLayer.Features.BLOG
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _blBlog;

        public BlogController()
        {
            _blBlog = new BL_Blog();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var lst = _blBlog.GetBlogs();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            var result = _blBlog.CreateBlog(blog);
            string message = result > 0 ? "New Blog Creation Successful" : "New Blog Creation Fail";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            var result = _blBlog.UpdateBlog(id, blog);
            string message = result > 0 ? "Blog update successful" : "Blog update fail";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            var result = _blBlog.PatchBlog(id, blog);
            string message = result > 0 ? "Blog update successful" : "Blog update fail";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            var result = _blBlog.DeleteBlog(id);
            string message = result > 0 ? "Blog delete successful" : "Blog delete fail";
            return Ok(message);
        }
    }
}
