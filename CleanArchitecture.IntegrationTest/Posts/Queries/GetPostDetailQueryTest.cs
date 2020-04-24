using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Posts.Queries.GetPostDetail;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.IntegrationTest.Posts.Queries
{

   using static SetupTestEnvironment;
   public class GetPostDetailQueryTest : TestBase
   {
      [Test]
      public async Task ShouldReturnAsociatedPostsDetail()
      {
         // Arrange
         await RunAsDefaultUserAsync();

         var firstPost = await AddAsync(new Post
         {
            DisplayName = "Test1",
            UserName = "userTest1",
            PhotoUrl = "someUrl",
            Title = "Test1 Title",
            Content = "Test1 Content for GetPostDetailQueryTest"
         });

         var secondPost = await AddAsync(new Post
         {
            DisplayName = "Test2",
            UserName = "userTest2",
            PhotoUrl = "someUrl",
            Title = "Test2 Title",
            Content = "Test2 Content for GetPostDetailQueryTest"
         });


         var queryForFirstPost = new GetPostDetailQuery() { Id = firstPost.Id };
         var queryForSecondPost = new GetPostDetailQuery() { Id = secondPost.Id };

         // Act
         var resultForFirstPost = await SendAsync(queryForFirstPost);
         var resultForSecondPost = await SendAsync(queryForSecondPost);

         // Assert
         resultForFirstPost.DisplayName.Should().Be(firstPost.DisplayName);
         resultForFirstPost.UserName.Should().Be(firstPost.UserName);
         resultForSecondPost.DisplayName.Should().Be(secondPost.DisplayName);
         resultForSecondPost.UserName.Should().Be(secondPost.UserName);
      }

      [Test]
      public async Task ShouldReturnExceptionOnInvalidRequest()
      {
         await RunAsDefaultUserAsync();
         // Arrange
         var query = new GetPostDetailQuery() { Id = new Guid() };

         FluentActions.Invoking(() => SendAsync(query))
            .Should().Throw<RestException>();
      }
   }
}