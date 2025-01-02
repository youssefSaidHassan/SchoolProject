using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Responses;

namespace SchoolProject.Core.Features.Authorization.Query.Models
{
    public class MangeUserClaimsQuery : IRequest<Response<MangeUserClaimsResponse>>
    {
        internal string userId;

        public string UserId { get; set; }
        public MangeUserClaimsQuery(string userId)
        {
            UserId = userId;
        }
    }
}
