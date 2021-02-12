using SevenDev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SevenDev.Domain.Interfaces
{
    public interface IGenderRepository
    {
        Task<Gender> GetByIdAsync(int id);
    }
}
