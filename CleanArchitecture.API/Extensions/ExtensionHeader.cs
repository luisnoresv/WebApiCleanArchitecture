using System.Text.Json;
using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Application.Common.Pagination;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.API.Extensions
{
  public static class ExtensionHeader
  {
    public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
    {
      var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
      var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
      response.Headers.Add(GlobalConstants.PAGINATION_HEADER, JsonSerializer.Serialize(paginationHeader, options));
      response.Headers.Add(GlobalConstants.ACCESS_CONTROL_EXPOSE_HEADERS, GlobalConstants.PAGINATION_HEADER);
    }
  }
}