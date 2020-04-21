using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Posts.Commands.CreatePost;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.IntegrationTest.Posts.Commands
{
   using static SetupTestEnvironment;

   public class CreatePostTest : TestBase
   {
      [Test]
      public void ShouldRequireMinimunFields()
      {
         var command = new CreatePostCommand();

         FluentActions.Invoking(() => SendAsync(command))
            .Should().Throw<Exception>();
      }

      [Test]
      public async Task ShouldCreatePost()
      {
         var currentUserName = await RunAsDefaultUserAsync();

         var command = new CreatePostCommand
         {
            DisplayName = "Test1",
            UserName = "postUserName",
            PhotoUrl = "someUrl",
            Title = "Test for Create",
            Content = "Create command Test"
         };

         var postId = await SendAsync(command);

         var newPost = await FindAsync<Post>(postId);

         newPost.Should().NotBeNull();
         newPost.DisplayName.Should().Be(command.DisplayName);
         newPost.UserName.Should().Be(command.UserName);
         newPost.PhotoUrl.Should().Be(command.PhotoUrl);
         newPost.Title.Should().Be(command.Title);
         newPost.Content.Should().Be(command.Content);
         newPost.Created.Should().BeCloseTo(DateTime.Now, 1000);
         newPost.CreatedBy.Should().Be(currentUserName);
      }
   }
}