namespace TimeManagerPortalReact.Data
{
    public class BaseConstants
    {
        public static class Roles
        {
            public const string Admin = "Admin";
            public const string User = "User";
            public const string Manager = "Manager";
        }
        public static class Policies
        {
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireUser = "RequireUser";
            public const string RequireManager = "RequireManager";
        }
    }
}
