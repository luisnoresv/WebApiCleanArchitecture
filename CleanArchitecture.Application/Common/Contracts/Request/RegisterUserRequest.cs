namespace CleanArchitecture.Application.Common.Contracts.Request
{
   public class RegisterUserRequest
   {
      public string UserName { get; set; }
      public string Email { get; set; }
      public string Password { get; set; }
   }
}