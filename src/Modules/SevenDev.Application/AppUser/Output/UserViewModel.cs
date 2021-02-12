using SevenDev.Domain.Entities;
using System;

namespace SevenDev.Application.AppUser.Output
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public string Photo { get; set; }
    }
}
