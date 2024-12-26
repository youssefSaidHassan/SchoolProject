using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Query.Responses;

namespace SchoolProject.Core.Features.Authorization.Query.Models
{
    public class GetRoleListQuery : IRequest<Response<List<GetRoleResponse>>>
    {
    }
}
