using AutoMapper;
using AutoMapper.QueryableExtensions;
using maxxyAPI.Data;
using maxxyAPI.DTOs;
using maxxyAPI.Entities;
using maxxyAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace maxxyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private Post _post = new Post();
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IPhotoService _photoService;

        public PostsController(UserManager<User> userManager, IMapper mapper, DataContext context, IPhotoService photoService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> Get()
        {
            try
            {
                List<Post> posts = await _context.Posts.ToListAsync();
                return Ok(posts);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PostDto>> Get([FromRoute] int id)
        {
            try
            {
                var post = await _context.Posts.Where(p => p.Id == id)
                    .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();
                return post == null? NotFound() : Ok(post);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<ActionResult<Post>> AddPost(PostDto post)
        {
            try
            {
                post.UserId = 1; // Admin

                _post = _mapper.Map<Post>(post);    
                await _context.Posts.AddAsync(_post);
                int response = await _context.SaveChangesAsync();
                if (response == 200)
                {
                    return Ok(_post);
                } else
                {
                    return BadRequest("Problem adding post");
                }

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<PostDto>> UpdatePost(PostDto request, [FromRoute] int id)
        {
            if (id != _post.Id) return BadRequest();

            _post = await _context.Posts.FindAsync(id);
            if (_post == null)
                return NotFound();

            try
            {
                _context.Entry(request).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                PostDto postDto = _mapper.Map<PostDto>(_post);

                return Ok(postDto);
            } catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete]
        public async Task<IActionResult> DeletePost(int id)
        {
            var postToDelete = await _context.Posts.FindAsync(id);
            if (postToDelete == null) return NotFound();

            _context.Posts.Remove(postToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
