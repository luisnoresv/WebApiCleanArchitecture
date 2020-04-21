using CleanArchitecture.Application.Common.Contracts.Request;
using CleanArchitecture.Application.Common.Pagination;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Posts.Queries.GetPosts
{
   public class GetPostsQuery : PaginationRequest, IRequest<PagedList<Post>> { }
}