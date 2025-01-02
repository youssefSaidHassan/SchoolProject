using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Query.Responses;

namespace SchoolProject.Core.Features.Authorization.Query.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleResponse>>
    {
        public string roleId { get; set; }
        public GetRoleByIdQuery(string roleId)
        {
            this.roleId = roleId;
        }
    }
}
