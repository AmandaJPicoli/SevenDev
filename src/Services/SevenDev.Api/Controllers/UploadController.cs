using SevenDev.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SevenDev.Application.AppPostage.Input;
using Microsoft.AspNetCore.Cors;

namespace SevenDev.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IStorageHelper _storageHelper;

        public UploadController(IStorageHelper storageHelper)
        {
            _storageHelper = storageHelper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Upload(IFormFile file) 
        {
            if (file is null)
                return BadRequest("Arquivo não carregado!");

            var format = file.FileName.Trim('\"');

            if (_storageHelper.IsImage(format))
            {
                if (file.Length > 0)
                {
                        return Ok(await _storageHelper.Upload(file, file.FileName));
                }
            }
            else
            {
                return new UnsupportedMediaTypeResult();
            }

            return BadRequest("Erro ao fazer o upload!");
        }
    }
}
