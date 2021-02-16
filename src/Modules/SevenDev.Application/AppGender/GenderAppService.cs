using SevenDev.Application.AppGender.Interfaces;
using SevenDev.Domain.Entities;
using SevenDev.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SevenDev.Application.AppGender
{
    public class GenderAppService : IGenderAppService
    {
        private readonly IGenderRepository _genderRepository;

        public GenderAppService(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<List<Gender>> GetAllGenders()
        {
            var genders = await _genderRepository
                                   .GetAllGenders()
                                   .ConfigureAwait(false);
            return genders;
        }
    }
}
