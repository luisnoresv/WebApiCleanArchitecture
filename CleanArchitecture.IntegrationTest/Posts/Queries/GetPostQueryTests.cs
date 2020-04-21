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
         var result = await SendAsync(query);

         // Assert
         result.Should().NotBeNull();
         result.TotalCount.Should().Be(1);
         result.CurrentPage.Should().Be(1);
         result.PageSize.Equals(1);
         result.TotalPages.Equals(1);
         result[0].DisplayName.Should().Be(addedPost.DisplayName);
         result[0].UserName.Should().Be(addedPost.UserName);
         result[0].PhotoUrl.Should().Be(addedPost.PhotoUrl);
         result[0].Title.Should().Be(addedPost.Title);
         result[0].Content.Should().Be(addedPost.Content);
      }
   }
}