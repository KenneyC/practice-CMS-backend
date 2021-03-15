using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using practice_CMS_backend.CMS;
using System.Threading.Tasks;

namespace practice_authentication_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogDataAccessProvider _dataAccessProvider;

        public BlogsController(IBlogDataAccessProvider provider)
        {
            _dataAccessProvider = provider;
        }

        [HttpGet]
        [Authorize]
        [Route("/getallblogs")]
        public IEnumerable<PostModel> GetAllBlogs()
        {
            return _dataAccessProvider.GetAllBlogs();
        }

        [HttpPost]
        [Authorize]
        [Route("/create")]
        public async Task<IActionResult> Create([FromBody] PostModel post)
        {
            bool createdPost = await _dataAccessProvider.AddBlog(post);

            if (createdPost) return Ok(new Response { Status = "Success", Message = "Created post successfully!" });
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Error creating post!" });
        }

        [HttpDelete]
        [Authorize]
        [Route("/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deletedPost = await _dataAccessProvider.DeleteBlog(id);

            if (deletedPost) return Ok(new Response { Status = "Success", Message = "Deleted post successfully!" });
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Error deleting post!" });
        }
    }
}