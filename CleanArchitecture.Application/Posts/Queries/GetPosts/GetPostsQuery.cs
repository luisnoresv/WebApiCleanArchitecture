using CleanArchitecture.Application.Common.Pagination;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Posts.Queries.GetPosts
{
    public class GetPostsQuery : IRequest<PagedList<Post>>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}