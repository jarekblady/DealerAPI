﻿namespace DealerAPI.Models
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; } = 1;
    }
}
