using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Persistence
{
   public static class ApplicationDbContextSeed
   {
      public static async Task SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
      {
         await SeedUserAsync(userManager);
         await AddSamplePosts(context);
      }

      private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
      {
         if (!userManager.Users.Any())
         {
            var appUser = new ApplicationUser { UserName = "admin", Email = "admin@localhost.com" };
            await userManager.CreateAsync(appUser);
         }
      }

      private static async Task AddSamplePosts(ApplicationDbContext context)
      {
         if (!context.Posts.Any())
         {
            var posts = new List<Post>
               {
                  new Post
                  {
                        DisplayName= "Ander",
                        UserName= "ander27",
                        PhotoUrl= "https://randomuser.me/api/portraits/men/19.jpg",
                        Title= "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
                        Content= "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
                  },
                  new Post
                  {
                        DisplayName= "Michael",
                        UserName= "deadHorse",
                        PhotoUrl= "https://randomuser.me/api/portraits/men/31.jpg",
                        Title= "qui est esse",
                        Content= "est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla"
                  },
                  new Post
                  {
                        DisplayName= "Daniel",
                        UserName= "dboy27",
                        PhotoUrl= "https://randomuser.me/api/portraits/men/45.jpg",
                        Title= "ea molestias quasi exercitationem repellat qui ipsa sit aut",
                        Content= "et iusto sed quo iure\nvoluptatem occaecati omnis eligendi aut ad\nvoluptatem doloribus vel accusantium quis pariatur\nmolestiae porro eius odio et labore et velit aut"
                  },
                  new Post
                  {
                        DisplayName= "Pierina",
                        UserName= "mprp2190",
                        PhotoUrl= "https://randomuser.me/api/portraits/women/19.jpg",
                        Title= "eum et est occaecati",
                        Content= "ullam et saepe reiciendis voluptatem adipisci\nsit amet autem assumenda provident rerum culpa\nquis hic commodi nesciunt rem tenetur doloremque ipsam iure\nquis sunt voluptatem rerum illo velit"
                  },
                  new Post
                  {
                        DisplayName= "Kattia",
                        UserName= "kvalente2343",
                        PhotoUrl= "https://randomuser.me/api/portraits/women/25.jpg",
                        Title= "nesciunt quas odio",
                        Content= "repudiandae veniam quaerat sunt sed\nalias aut fugiat sit autem sed est\nvoluptatem omnis possimus esse voluptatibus quis\nest aut tenetur dolor neque"
                  },
                  new Post
                  {
                        DisplayName= "Rafaela",
                        UserName= "rafa34k",
                        PhotoUrl= "https://randomuser.me/api/portraits/women/89.jpg",
                        Title= "dolorem eum magni eos aperiam quia",
                        Content= "ut aspernatur corporis harum nihil quis provident sequi\nmollitia nobis aliquid molestiae\nperspiciatis et ea nemo ab reprehenderit accusantium quas\nvoluptate dolores velit et doloremque molestiae"
                    }
                };

            context.Posts.AddRange(posts);
            await context.SaveChangesAsync();
         }
      }
   }
}