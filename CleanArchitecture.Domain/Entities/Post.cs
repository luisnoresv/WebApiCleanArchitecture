using System;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
   public class Post : AuditableEntity
   {
      public Guid Id { get; set; }
      public string DisplayName { get; set; }
      public string UserName { get; set; }
      public string PhotoUrl { get; set; }
      public string Title { get; set; }
      public string Content { get; set; }
   }
}