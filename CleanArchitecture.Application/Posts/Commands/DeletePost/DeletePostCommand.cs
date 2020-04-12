using System;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}