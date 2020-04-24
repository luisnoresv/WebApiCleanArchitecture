using System.Threading.Tasks;
using CleanArchitecture.Application.Posts.Queries.GetPosts;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;


namespace CleanArchitecture.IntegrationTest.Posts.Queries
{
   using static SetupTestEnvironment;
   public class GetPostQueryTests : TestBase
   {
      [Test]
      public async Task ShouldReturnAsociatedPostsPaging()
      {
         // Arrange
         await RunAsDefaultUserAsync();

         var addedPost = await AddAsync(new Post
         {
            DisplayName = "Test1",
            UserName = "userTest1",
            PhotoUrl = "someUrl",
            Title = "Test1 Title",
            Content = "Test1 Content for GetPostQueryTest"
         });

         var query = new GetPostsQuery() { CurrentPage = 0, PageSize = 4 };

         // Act
         var postPagingResult = await SendAsync(query);

         // Assert
         foreach (var postItem in postPagingResult)
         {
            postItem.DisplayName.Should().Be(addedPost.DisplayName);
            postItem.UserName.Should().Be(addedPost.UserName);
            postItem.PhotoUrl.Should().Be(addedPost.PhotoUrl);
            postItem.Title.Should().Be(addedPost.Title);
            postItem.Content.Should().Be(addedPost.Content);
         }

         postPagingResult.Should().NotBeNull();
         postPagingResult.TotalCount.Should().Be(1);
         postPagingResult.CurrentPage.Should().Be(1);
         postPagingResult.PageSize.Should().Be(3);
         postPagingResult.TotalPages.Should().Be(1);

      }
   }
}