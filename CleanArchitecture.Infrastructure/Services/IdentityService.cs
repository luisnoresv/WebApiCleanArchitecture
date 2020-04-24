using System;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Application.Common.Contracts.Request;
using CleanArchitecture.Application.Common.Contracts.Response;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Services
{
   public class IdentityService : IIdentityService
   {
      private readonly UserManager<ApplicationUser> _userManager;
      private readonly ITokenService _jwtGenerator;
      private readonly ICurrentUserService _currentUserService;
      private readonly SignInManager<ApplicationUser> _signInManager;
      public IdentityService(UserManager<ApplicationUser> userManager,
           ITokenService tokenService, ICurrentUserService currentUserService, SignInManager<ApplicationUser> signInManager)
      {
         _signInManager = signInManager;
         _currentUserService = currentUserService;
         _jwtGenerator = tokenService;
         _userManager = userManager;

      }

      public async Task<UserResponse> GetCurrentUser()
      {
         var user = await _userManager.FindByNameAsync(_currentUserService.GetCurrentUserName());

         if (user == null)
            throw new RestException(HttpStatusCode.Unauthorized);

         return new UserResponse
         {
            UserName = user.UserName,
            Token = await _jwtGenerator.CreateToken(user)
         };
      }

      public async Task<UserResponse> Login(LoginUserRequest request)
      {
         var user = await _userManager.FindByEmailAsync(request.Email);

         if (user == null)
            throw new RestException(HttpStatusCode.Unauthorized);

         var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

         if (result.Succeeded)
         {
            return new UserResponse
            {
               Token = await _jwtGenerator.CreateToken(user),
               UserName = user.UserName
            };
         }

         throw new RestException(HttpStatusCode.Unauthorized);
      }

      public async Task<UserResponse> Register(RegisterUserRequest request)
      {
         if (await _userManager.FindByEmailAsync(request.Email) != null)
            throw new RestException(HttpStatusCode.BadRequest, new { Email = GlobalConstants.DUPLICATE_EMAIL });

         if (await _userManager.FindByNameAsync(request.UserName) != null)
            throw new RestException(HttpStatusCode.BadRequest, new { UserName = GlobalConstants.DUPLICATE_USERNAME });

         var user = new ApplicationUser
         {
            Email = request.Email,
            UserName = request.UserName
         };

         var result = await _userManager.CreateAsync(user, request.Password);

         if (result.Succeeded)
         {
            await _userManager.AddToRoleAsync(user, GlobalConstants.MEMBER_ROLE);

            return new UserResponse
            {
               UserName = user.UserName,
               Token = await _jwtGenerator.CreateToken(user)
            };
         }

         throw new Exception(GlobalConstants.ERROR_CREATING_USER);
      }
   }
}