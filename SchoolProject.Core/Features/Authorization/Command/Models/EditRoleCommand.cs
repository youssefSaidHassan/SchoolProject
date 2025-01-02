using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Requests;

namespace SchoolProject.Core.Features.Authorization.Command.Models
{
    public class EditRoleCommand : EditRoleRequest, IRequest<Response<string>>
    {

    }
}
