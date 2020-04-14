using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.API.Extensions;
using CleanArchitecture.Application.Common.Contracts.Request;
using CleanArchitecture.Application.Common.Contracts.Response;
using CleanArchitecture.Application.Posts.Commands.CreatePost;
using CleanArchitecture.Application.Posts.Commands.DeletePost;
using CleanArchitecture.Application.Posts.Commands.UpdatePost;
using CleanArchitecture.Application.Posts.Queries.GetPostDetail;
using CleanArchitecture.Application.Posts.Queries.GetPosts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    public class PostController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResponse>>> List([FromQuery]PaginationRequest request)
        {
            var posts = await Mediator.Send(new GetPostsQuery()
            {
                CurrentPage = request.CurrentPage,
                PageSize = request.PageSize
            });

            var viewModelList = new List<PostResponse>();

            foreach (var post in posts)
            {
                var postViewModel = new PostResponse
                {
                    Id = post.Id.ToString(),
                    Content = post.Content,
                    DisplayName = post.DisplayName,
                    UserName = post.UserName,
                    PhotoUrl = post.PhotoUrl,
                    Title = post.Title,
                    PostedOn = post.CreatedUtc.ToShortDateString()
                };
                viewModelList.Add(postViewModel);
            }

            Response.AddPagination(posts.CurrentPage, posts.PageSize, posts.TotalCount, posts.TotalPages);
            return Ok(viewModelList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponse>> Detail([FromRoute]Guid id)
        {
            return await Mediator.Send(new GetPostDetailQuery() { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreatePostCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit([FromRoute]Guid id, UpdatePostCommand command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete([FromRoute]Guid id)
        {
            return await Mediator.Send(new DeletePostCommand() { Id = id });
        }
    }
}