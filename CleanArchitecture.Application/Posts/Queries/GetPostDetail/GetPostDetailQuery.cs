using System;
using CleanArchitecture.Application.Common.Contracts.Response;
using MediatR;

namespace CleanArchitecture.Application.Posts.Queries.GetPostDetail
{
    public class GetPostDetailQuery : IRequest<PostResponse>
    {
        public Guid Id { get; set; }
    }
}