﻿namespace ServerWebAPI.DTOs
{
    public class RegisterDTO
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int NumEmployees { get; set; }
        public bool IsVip { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmed { get; set; }
    }
}
