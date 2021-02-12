using System;
using System.Collections.Generic;
using System.Text;

namespace SevenDev.Application.AppUser.Input
{
    public class UserUpdateInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int GenderId { get; set; }
        public string Photo { get; set; }
    }
}
