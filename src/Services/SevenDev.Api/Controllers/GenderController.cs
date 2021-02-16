using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SevenDev.Application.AppGender.Interfaces;
using System;
using System.Threading.Tasks;

namespace SevenDev.Api.Controllers
{
    [EnableCors("MyPolicy")]
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
        [HttpGet("")]
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
