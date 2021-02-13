using SevenDev.Application.AppPostage.Input;
using SevenDev.Application.AppPostage.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDev.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostageController : ControllerBase
    {
        private readonly IPostageAppService _postageAppService;
        private readonly ICommentAppService _commentAppService;
        private readonly ILikesAppService _likesAppService;
        public PostageController(IPostageAppService postageAppService,
                                  ICommentAppService commentAppService,
                                  ILikesAppService likesAppService)
        {
            _postageAppService = postageAppService;
            _commentAppService = commentAppService;
            _likesAppService = likesAppService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostageInput postageInput)
        {
            try
            {
                var postage = await _postageAppService
                                    .InsertAsync(postageInput)
                                    .ConfigureAwait(false);

                return Created("", postage);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var postages = await _postageAppService
                                    .GetPostageByUserIdAsync()
                                    .ConfigureAwait(false);

            if (postages is null)
                return NoContent();

            return Ok(postages);
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/Comments")]
        public async Task<IActionResult> PostCommets([FromRoute] int id, [FromBody] CommentInput commentInput)
        {
            try
            {
                var user = await _commentAppService
                                    .InsertAsync(id, commentInput)
                                    .ConfigureAwait(false);

                return Created("", user);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/Comments")]
        public async Task<IActionResult> GetComments([FromRoute] int id)
        {
            var comments = await _commentAppService
                                    .GetByPostageIdAsync(id)
                                    .ConfigureAwait(false);

            if (comments is null)
                return NoContent();

            return Ok(comments);
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/Likes")]
        public async Task<IActionResult> PostLike([FromRoute] int id)
        {
            try
            {
                await _likesAppService
                            .InsertOrDeleteAsync(id)
                            .ConfigureAwait(false);

                return Created("", "");
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/Likes/Quantity")]
        public async Task<IActionResult> GetLike([FromRoute] int id)
        {
            try
            {
                var quantity = await _likesAppService
                                        .GetQuantityOfLikesByPostageIdAsync(id)
                                        .ConfigureAwait(false);

                return Ok(quantity);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Album")]
        public async Task<IActionResult> GetAlbum()
        {
            try
            {
                var album = await _postageAppService
                                        .GetAlbum()
                                        .ConfigureAwait(false);

                return Ok(album);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }
    }
}
