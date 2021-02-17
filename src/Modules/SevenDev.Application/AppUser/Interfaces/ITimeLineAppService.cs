using SevenDev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SevenDev.Application.AppUser.Interfaces
{
    public interface ITimeLineAppService
    {
        Task<List<PostagesTimeLine>> GetTimeLineByUserId();
    }
}
