using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Responses;

namespace SchoolProject.Core.Features.Authorization.Query.Models
{
    public class MangeUserRolesQuery : IRequest<Response<MangeUserRolesResponse>>
    {
        public string userId { get; set; }
        public MangeUserRolesQuery(string userId)
        {
            this.userId = userId;
        }
    }
}
