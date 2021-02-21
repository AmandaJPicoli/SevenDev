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
using Microsoft.AspNetCore.Cors;
using SevenDev.Application.AppPostage.Interfaces;
using SevenDev.Domain.Entities;

namespace SevenDev.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly ITimeLineAppService _timeLineAppService;

        public UserController(IUserAppService userAppService, ITimeLineAppService timeLineAppService)
        {
            _userAppService = userAppService;
            _timeLineAppService = timeLineAppService;
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

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserUpdateInput userUpdateInput)
        {
            try
            {
                var user = await _userAppService
                                    .UpdateAsync(userUpdateInput)
                                    .ConfigureAwait(false);

                return Ok(user);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpPost("InviteFriends")]
        public async Task<IActionResult> PostInvite([FromQuery] int userIdReceive) 
        {
            try
            {
                var inviteReturn = await _userAppService
                                        .InsertInviteAsync(userIdReceive)
                                        .ConfigureAwait(false);
                
                return Created("", inviteReturn);
            }
            catch (ApplicationException ex)
            {
                var error = new { erro = ex.Message };
                return Conflict(error);
            }
        }

        [Authorize]
        [HttpGet("TimeLine")]
        public async Task<IActionResult> GetTimeLine()
        {
            try
            {
                var timeLine = await _timeLineAppService
                                        .GetTimeLineByUserId()
                                        .ConfigureAwait(false);
                return Ok(timeLine);
            }
            catch (Exception ex)
            {
                var error = new { error = ex.Message };
                return BadRequest(error);
            }
        }

        [Authorize]
        [HttpPut("AcceptDeniedInvite")]
        public async Task<IActionResult> PutInvite([FromBody] InviteFriends invite)
        {
            try
            {
                var inviteResponse = await _userAppService
                                    .AcceptDeniedInvite(invite)
                                    .ConfigureAwait(false);

                return Ok(inviteResponse);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet("InviteReceive")]
        public async Task<IActionResult> GetAllInvitesReceiveById()
        {
            try
            {
                var invites = await _userAppService
                                   .GetAllInvitesReceive()
                                   .ConfigureAwait(false);
                return Ok(invites);
            }
            catch (Exception ex)
            {
                var error = new { error = ex.Message };
                return BadRequest(error);
            }
        }

    }
}
