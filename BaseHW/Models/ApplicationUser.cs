﻿using Microsoft.AspNetCore.Identity;

namespace BaseHW.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public List<Post>? Posts { get; set; }

    }
}
