using Microsoft.AspNetCore.Mvc;
using Gifter.Repositories;
using Gifter.Models;
using System;

namespace Gifter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }





        // https://localhost:5001/api/post/
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postRepository.GetAll());
        }





        // https://localhost:5001/api/post/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var post = _postRepository.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }





        // https://localhost:5001/api/post/
        [HttpPost]
        public IActionResult Post(Post post)
        {
            _postRepository.Add(post);
            return CreatedAtAction("Get", new { id = post.Id }, post);
        }





        // https://localhost:5001/api/post/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _postRepository.Update(post);
            return NoContent();
        }





        [HttpGet("GetAllWithComments")]
        public IActionResult GetAllWithComments()
        {
            var posts = _postRepository.GetAllWithComments();
            return Ok(posts);
        }




        [HttpGet("GetPostByIdWithComments")]
        public IActionResult GetPostByIdWithComments(int id)
        {
            var post = _postRepository.GetPostByIdWithComments(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }




        // https://localhost:5001/api/post/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _postRepository.Delete(id);
            return NoContent();
        }





        [HttpGet("search")]
        public IActionResult Search(string q, bool sortDesc)
        {
            return Ok(_postRepository.Search(q, sortDesc));
        }





        [HttpGet("hottest")]
        public IActionResult Search(DateTime since, bool sortDesc)
        {
            return Ok(_postRepository.Search(since, sortDesc));
        }
    }
}