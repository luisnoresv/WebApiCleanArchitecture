using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Application.Common.Contracts.Response;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Posts.Queries.GetPostDetail
{
   public class GetPostDetailQueryHandler : IRequestHandler<GetPostDetailQuery, PostResponse>
   {
      private readonly IApplicationDbContext _context;
      public GetPostDetailQueryHandler(IApplicationDbContext context)
      {
         _context = context;
      }

      public async Task<PostResponse> Handle(GetPostDetailQuery request, CancellationToken cancellationToken)
      {
         var entity = await _context.Set<Post>().FindAsync(request.Id);

         if (entity == null)
            throw new RestException(HttpStatusCode.NotFound, new { Todo = GlobalConstants.NOT_FOUND });

         var response = new PostResponse
         {
            Id = entity.Id.ToString(),
            Content = entity.Content,
            DisplayName = entity.DisplayName,
            UserName = entity.UserName,
            PhotoUrl = entity.PhotoUrl,
            Title = entity.Title
         };

         return response;
      }
   }
}