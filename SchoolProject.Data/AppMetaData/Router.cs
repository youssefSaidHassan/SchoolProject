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

            public const string ChangePassword = $"{Prefix}Change-Password";
            public const string Create = $"{Prefix}Create";
            public const string Edit = $"{Prefix}Edit";
            public const string Delete = $"{Prefix}Delete/{{id}}";



        }

    }
}
