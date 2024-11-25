﻿using Core.AOP.Aspects;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using TechCareer.Service.Abstracts;
using TechCareer.Service.Rules;
using TechCareer.Service.Validations.Users;

namespace TechCareer.Service.Concretes;

public sealed class AuthService(
    UserBusinessRules _rules, 
    IUserWithTokenService _tokenService,
    IUserService _userService
    ) : IAuthService
{

    [ValidationAspect(typeof(LoginValidator))]
    [LoggerAspect]
    public async Task<AccessToken> LoginAsync(UserForLoginDto dto,CancellationToken cancellationToken)
    {
        User? user = await _userService.GetAsync(
            predicate: u => u.Email == dto.Email,
            cancellationToken: cancellationToken
        );
        
        await _rules.UserShouldBeExistsWhenSelected(user);
        await _rules.UserPasswordShouldBeMatched(user!, dto.Password);
        
        AccessToken createdAccessToken = await _tokenService.CreateAccessToken(user!);

        return createdAccessToken;

    }

    public async Task<AccessToken> RegisterAsync(UserForRegisterDto dto,CancellationToken cancellationToken)
    {
        HashingHelper.CreatePasswordHash(
            dto.Password,
            passwordHash: out byte[] passwordHash,
            passwordSalt: out byte[] passwordSalt
        );
        
      User  newUser =
            new()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
      
      User createdUser = await _userService.AddAsync(newUser);
      AccessToken createdAccessToken = await _tokenService.CreateAccessToken(createdUser);

      return createdAccessToken;
    }
}