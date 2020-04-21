using System;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.Application.Posts.Commands.CreatePost
{
   public class CreatePostCommand : IRequest<Guid>
   {
      public string DisplayName { get; set; }
      public string UserName { get; set; }
      public string PhotoUrl { get; set; }
      public string Title { get; set; }
      public string Content { get; set; }
   }

   public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
   {
      public CreatePostCommandValidator()
      {
         RuleFor(x => x.DisplayName).NotEmpty();
         RuleFor(x => x.UserName).NotEmpty();
         RuleFor(x => x.PhotoUrl).NotEmpty();
         RuleFor(x => x.Title).NotEmpty();
         RuleFor(x => x.Content).NotEmpty();
      }
   }
}