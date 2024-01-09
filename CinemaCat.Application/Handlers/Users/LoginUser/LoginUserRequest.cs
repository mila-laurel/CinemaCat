﻿using MediatR;

namespace CinemaCat.Application.Handlers.Users.LoginUser;
public class LoginUserRequest : IRequest<LoginUserResponse>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
