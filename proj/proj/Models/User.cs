﻿namespace proj.Models;

public class User
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int? CartId { get; set; }
    public string Role { get; set; }
}