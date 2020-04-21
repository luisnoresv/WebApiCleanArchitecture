namespace CleanArchitecture.Application.Common.Constants
{
   public static class GlobalConstants
   {
      // Environments Variables
      public const string APP_SETTINGS_FILE = "appsettings.json";
      public const string APPLICATION_NAME = "CleanArchitecture.API";
      public const string ENVIRONMENT_NAME = "Development";
      public const string MIGRATION_TABLE = "__EFMigrationsHistory";
      public const string EF_CONNECTION_STRING = "DefaultConnection";
      // keys
      public const string TOKEN_KEY_SECTION = "TokenKey";

      // API Messages
      public const string ERROR_SAVING_CHANGES = "Problem saving changes";
      public const string NOT_FOUND = "Not Found";
      public const string ERROR_CREATING_USER = "Error on creating the user";
      public const string ERROR_DELETE_MAIN_PHOTO = "You cannot delete your main photo";
      public const string ERROR_DELETE_PHOTO = "Problem deleting the photo";
      public const string ERROR_SET_MAIN = "You cannot set to Main your Main Photo";
      // Errors
      public const string REST_ERROR = "REST ERROR";
      public const string SERVER_ERROR = "SERVER ERROR";

      // Types
      public const string APPLICATION_TYPE_JSON = "application/json";

      // Headers
      public const string PAGINATION_HEADER = "Pagination";
      public const string ACCESS_CONTROL_EXPOSE_HEADERS = "Access-Control-Expose-Headers";
      public const string WWW_Authenticate = "WWW-Authenticate";

      // Roles
      public const string ADMIN_ROLE = "Admin";
      public const string MODERATOR_ROLE = "Moderator";
      public const string MEMBER_ROLE = "Member";

      // Policies
      public const string REQUIRE_ADMIN_ROLE = "RequireAdminRole";
      public const string REQUIRE_MODERATOR_ROLE = "RequireModeratorRole";
      public const string CORS_POLICY = "CorsPolicy";

      // White List Domains
      public const string LOCAL_HOST_DOMAIN = "http://localhost:3000";

      // SPA File Path
      public const string SPA_PATH = "wwwroot";
      public const string SPA_INDEX_FILE = "index.html";
      public const string SPA_CONTENT_TYPE = "text/Html";
   }
}