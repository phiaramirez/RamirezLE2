using BlogDataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public PostController()
        {

        }

        [Authorize]
        [HttpGet]
        [Route("ListPosts")]
        public IActionResult ListPosts()
        {
            var posts = new[]
            {
                new
                {
                    Id = 1,
                    Title = "First Post",
                    Body = "This is the first blog post."
                },
                new
                {
                    Id = 2,
                    Title = "Second Post",
                    Body = "This is the second blog post."
                }
            };

            return Ok(posts);
        }

        [Authorize]
        [HttpGet]
        [Route("details/{id}")]
        public IActionResult ShowPostDetails(int id)
        {
            var posts = new[]
            {
                new
                {
                    Id = 1,
                    Title = "First Post",
                    Body = "This is the first blog post."
                },
                new
                {
                    Id = 2,
                    Title = "Second Post",
                    Body = "This is the second blog post."
                }
            };

            var post = posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        private int GetCurrentUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                string id = claims.FirstOrDefault(
                    x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                return int.Parse(id);
            }

            return 0;
        }

        [Authorize]
        [HttpPost]
        [Route("AddPost")]
        public IActionResult AddPost(PostForm postForm)
        {
            try
            {
                int userId = GetCurrentUserId();

                var newPost = new
                {
                    UserId = userId,
                    Title = postForm.Title,
                    Body = postForm.Body
                };

                return Ok(newPost);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}