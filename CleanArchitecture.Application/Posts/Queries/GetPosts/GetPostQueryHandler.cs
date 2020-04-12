using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Pagination;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Posts.Queries.GetPosts
{
    public class GetPostQueryHandler : IRequestHandler<GetPostsQuery, PagedList<Post>>
    {
        private readonly IApplicationDbContext _context;
        public GetPostQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Post>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.Set<Post>().AsQueryable();

            return await PagedList<Post>.CreateAsync(queryable, request.CurrentPage, request.PageSize);
        }
    }
}