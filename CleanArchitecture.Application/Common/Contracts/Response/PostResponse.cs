namespace CleanArchitecture.Application.Common.Contracts.Response
{
    public class PostResponse
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}