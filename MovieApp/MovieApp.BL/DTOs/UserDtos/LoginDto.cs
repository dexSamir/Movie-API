﻿namespace MovieApp.BL.DTOs.UserDtos;
public class LoginDto
{
	public string UsernameOrEmail { get; set; }
	public string Password { get; set; }
	public bool RememberMe { get; set; }

}

