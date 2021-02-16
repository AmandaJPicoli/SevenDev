using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SevenDev.Application.AppGender.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDev.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderAppService _genderAppService;

        public GenderController(IGenderAppService genderAppService)
        {
            _genderAppService = genderAppService;
        }

        [AllowAnonymous]
        [HttpGet("AllGenders")]
        public async Task<IActionResult> GetAllGenders()
        {
            try
            {
                var genders = await _genderAppService
                                        .GetAllGenders()
                                        .ConfigureAwait(false);

                if(genders == null)
                {
                    return NotFound();
                }
                return Ok(genders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
