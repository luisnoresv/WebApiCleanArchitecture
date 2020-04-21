namespace CleanArchitecture.Application.Common.Contracts.Request
{
   public class PaginationRequest
   {
      private const int MAX_PAGE_SIZE = 3;

      private int currentPage = 1;
      public int CurrentPage
      {
         get => currentPage;
         set => currentPage = (value == 0) ? currentPage : value;
      }
      private int pageSize = 3;
      public int PageSize
      {
         get => pageSize;
         set => pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
      }
   }
}