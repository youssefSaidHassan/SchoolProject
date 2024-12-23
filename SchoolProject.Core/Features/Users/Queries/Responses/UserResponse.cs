﻿namespace SchoolProject.Core.Features.Users.Queries.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
