using SevenDev.Application.AppUser.Input;
using SevenDev.Application.AppUser.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDev.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserInput userInput)
        {
            try
            {
                var user = await _userAppService
                                    .InsertAsync(userInput)
                                    .ConfigureAwait(false);

                return Created("", user);
            }
            catch(ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var user = await _userAppService
                                .GetByIdAsync(id)
                                .ConfigureAwait(false);

            if (user is null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UserUpdateInput userUpdateInput)
        {
            try
            {
                var user = await _userAppService
                                    .UpdateAsync(id, userUpdateInput)
                                    .ConfigureAwait(false);

                return Ok(user);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [HttpPost("ConvidarAmigo")]
        public async Task<IActionResult> PostInvite([FromBody] int userIdReceive) 
        {
            try
            {
                var inviteReturn = await _userAppService
                                        .InsertInviteAsync(userIdReceive)
                                        .ConfigureAwait(false);
                
                return Created("", inviteReturn);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        } 

    }
}
