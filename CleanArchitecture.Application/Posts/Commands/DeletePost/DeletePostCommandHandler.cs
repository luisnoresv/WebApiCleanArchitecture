using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Application.Common.Error;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Posts.Commands.DeletePost
{
   public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
   {
      private readonly IApplicationDbContext _context;
      public DeletePostCommandHandler(IApplicationDbContext context)
      {
         _context = context;
      }

      public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
      {
         var entity = await _context.Set<Post>().FindAsync(request.Id);

         if (entity == null) throw new RestException(HttpStatusCode.NotFound, new { Post = GlobalConstants.NOT_FOUND });

         _context.Set<Post>().Remove(entity);

         var success = await _context.SaveChangesAsync(cancellationToken) > 0;

         if (success) return Unit.Value;

         throw new Exception(GlobalConstants.ERROR_SAVING_CHANGES);
      }
   }
}