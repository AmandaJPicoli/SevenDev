using SevenDev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SevenDev.Application.AppGender.Interfaces
{
    public interface IGenderAppService
    {
        Task<List<Gender>> GetAllGenders();
    }
}
