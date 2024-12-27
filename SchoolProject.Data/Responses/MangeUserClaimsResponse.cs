namespace SchoolProject.Data.Responses
{
    public class MangeUserClaimsResponse
    {
        public MangeUserClaimsResponse()
        {
            Claims = new List<UserClaims>();
        }
        public string UserId { get; set; }
        public List<UserClaims> Claims { get; set; }
    }
    public class UserClaims
    {
        public string Type { get; set; }
        public bool Value { get; set; }
    }
}
