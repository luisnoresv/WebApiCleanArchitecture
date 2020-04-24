using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Posts.Commands.UpdatePost;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.IntegrationTest.Posts.Commands
{
   using static SetupTestEnvironment;

   public class UpdatePostTests : TestBase
   {
      [Test]
      public async Task ShouldRequireMinimunFields()
      {
         await RunAsDefaultUserAsync();

         var command = new UpdatePostCommand();

         FluentActions.Invoking(() => SendAsync(command))
            .Should().Throw<ValidationException>();
      }

      [Test]
      public async Task ShouldUpdatePost()
      {
         var currentUserName = await RunAsDefaultUserAsync();

         var addedPost = await AddAsync(new Post
         {
            DisplayName = "AddedPost",
            UserName = "userTest1",
            PhotoUrl = "someUrl",
            Title = "New Post Title",
            Content = "New Post Content for Test"
         });

         var command = new UpdatePostCommand
         {
            Id = addedPost.Id,
            DisplayName = "UpdatedPost",
            UserName = "updateUserName",
            PhotoUrl = "anotherUrl",
            Title = "Update Post Title",
            Content = "Update Post Content for Test"
         };

         await SendAsync(command);

         var updatePost = await FindAsync<Post>(addedPost.Id);

         updatePost.Should().NotBeNull();
         updatePost.DisplayName.Should().Be(command.DisplayName);
         updatePost.UserName.Should().Be(command.UserName);
         updatePost.PhotoUrl.Should().Be(command.PhotoUrl);
         updatePost.Title.Should().Be(command.Title);
         updatePost.Content.Should().Be(command.Content);
         updatePost.LastModified.Should().BeCloseTo(DateTime.Now, 1000);
         updatePost.LastModifiedBy.Should().Be(currentUserName);
      }
   }
}