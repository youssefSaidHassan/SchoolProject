namespace SchoolProject.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string rule = $"{root}/{version}/";
        public static class StudentRouting
        {
            public const string Prefix = $"{rule}Student/";
            public const string List = $"{Prefix}List";
            public const string Paginated = $"{Prefix}Paginated";
            public const string GetById = $"{Prefix}{{id}}";
            public const string Create = $"{Prefix}Create";
            public const string Edit = $"{Prefix}Edit";
            public const string Delete = $"{Prefix}Delete/{{id}}";



        }
        public static class DepartmentRouting
        {
            public const string Prefix = $"{rule}Deaprtment/";
            public const string List = $"{Prefix}List";
            public const string Paginated = $"{Prefix}Paginated";
            public const string GetById = $"{Prefix}Id";
            public const string Create = $"{Prefix}Create";
            public const string Edit = $"{Prefix}Edit";
            public const string Delete = $"{Prefix}Delete/{{id}}";



        }
        public static class UserRouting
        {
            public const string Prefix = $"{rule}User/";
            public const string List = $"{Prefix}List";
            public const string Paginated = $"{Prefix}Paginated";
            public const string GetById = $"{Prefix}{{id}}";

            public const string Create = $"{Prefix}Create";
            public const string Edit = $"{Prefix}Edit";
            public const string Delete = $"{Prefix}Delete/{{id}}";
        }
        public static class AuthenticationRouting
        {
            public const string Prefix = $"{rule}Authentication/";
            public const string SignIn = $"{Prefix}SignIn";
            public const string RefreshToken = $"{Prefix}RefreshToken";
            public const string ValidateToken = $"{Prefix}ValidateToken";

        }
        public static class AuthorizationRouting
        {
            public const string Prefix = $"{rule}Authorization/";
            public const string Role = $"{Prefix}Role";
            public const string Claims = $"{Prefix}Claims";
            public const string CreateRole = $"{Role}/Create";
            public const string DeleteRole = $"{Role}/Delete/{{id}}";
            public const string EditRole = $"{Role}/Edit";
            public const string GetAll = $"{Role}GetAll";
            public const string GetById = $"{Role}GetById/{{id}}";
            public const string MangeUserRoles = $"{Role}MangeUserRoles/{{userId}}";
            public const string UpdateUserRoles = $"{Role}UpdateUserRoles";
            public const string MangeUserClaims = $"{Claims}MangeUserClaims/{{userId}}";
            public const string UpdateUserClaims = $"{Claims}/UpdateUserClaims";

        }

    }
}
