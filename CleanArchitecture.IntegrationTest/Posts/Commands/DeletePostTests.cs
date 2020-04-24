using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Posts.Commands.CreatePost;
using CleanArchitecture.Application.Posts.Commands.DeletePost;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.IntegrationTest.Posts.Commands
{
   using static SetupTestEnvironment;

   public class DeletePostTests : TestBase
   {
      [Test]
      public async Task ShouldRequireMinimunFields()
      {
         await RunAsDefaultUserAsync();
         var command = new DeletePostCommand();

         FluentActions.Invoking(() => SendAsync(command))
            .Should().Throw<ValidationException>();
      }

      [Test]
      public async Task ShouldDeleteSelectedPost()
      {
         await RunAsDefaultUserAsync();

         var postId = await SendAsync(new CreatePostCommand
         {
            DisplayName = "userName",
            UserName = "postUserName",
            PhotoUrl = "someUrl",
            Title = "Test for Delete",
            Content = "Delete command Test"
         });

         await SendAsync(new DeletePostCommand
         {
            Id = postId
         });

         var list = await FindAsync<Post>(postId);

         list.Should().BeNull();
      }
   }
}