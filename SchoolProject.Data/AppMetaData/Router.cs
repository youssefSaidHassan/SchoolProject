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
            public const string CreateRole = $"{Prefix}Role/Create";
            public const string DeleteRole = $"{Prefix}Role/Delete/{{id}}";
            public const string EditRole = $"{Prefix}Role/Edit";
            public const string GetAll = $"{Prefix}GetAll";
            public const string GetById = $"{Prefix}GetById/{{id}}";

        }

    }
}
