namespace SchoolProject.Data.DTOs
{
    public class MangeUserRolesResponse
    {
        public MangeUserRolesResponse()
        {
            Roles = new List<UserRoles>();
        }
        public string UserId { get; set; }
        public List<UserRoles> Roles { get; set; }
    }
    public class UserRoles
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool HasRole { get; set; }
    }
}
