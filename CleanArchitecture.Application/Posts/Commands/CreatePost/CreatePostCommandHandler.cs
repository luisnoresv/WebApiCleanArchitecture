using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Posts.Commands.CreatePost
{
  public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
  {
    private readonly IApplicationDbContext _context;
    public CreatePostCommandHandler(IApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
      var entity = new Post
      {
        Content = request.Content,
        DisplayName = request.DisplayName,
        UserName = request.UserName,
        PhotoUrl = request.PhotoUrl,
        Title = request.Title
      };

      _context.Set<Post>().Add(entity);

      await _context.SaveChangesAsync(cancellationToken);

      return entity.Id;

      throw new Exception(GlobalConstants.ERROR_SAVING_CHANGES);
    }
  }
}