using AutoMapper;
using AutoMapper.QueryableExtensions;
using maxxyAPI.Data;
using maxxyAPI.DTOs;
using maxxyAPI.Entities;
using maxxyAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace maxxyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireAdminRole")]
    public class ImagesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public ImagesController(DataContext context, IMapper mapper, IPhotoService photoService)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Image>>> Get()
        {
            try
            {
                List<Image> images = await _context.Images.ToListAsync();
                return Ok(images);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Image>> GetImage([FromRoute] int id)
        {
            try
            {
                ImageDto image = await _context.Images.Where(i => i.Id == id)
                    .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync();
                if (image == null)
                    return NotFound();
                return Ok(image);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> AddImage(IFormFile file, [FromQuery] int idPost)
        {
            try
            {
                Post post = await _context.Posts.Where(p => p.Id == idPost)
                    .SingleOrDefaultAsync();
                if (post == null)
                    return NotFound();

                var result = await _photoService.AddPhotoAsync(file);
                Image img = new()
                {
                    Url = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId,
                    PostId = post.Id
                };

                await _context.Images.AddAsync(img);
                await _context.SaveChangesAsync();

                PostDto postDto = _mapper.Map<PostDto>(post);

                return Ok(postDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            Image img = await _context.Images.FindAsync(id);
            if (img == null) 
                return NotFound();
            _context.Images.Remove(img);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
