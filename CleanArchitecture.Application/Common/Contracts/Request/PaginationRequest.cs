namespace CleanArchitecture.Application.Common.Contracts.Request
{
    public class PaginationRequest
    {
        private const int MAX_PAGE_SIZE = 3;
        public int CurrentPage { get; set; } = 1;
        private int pageSize = 3;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }
    }
}