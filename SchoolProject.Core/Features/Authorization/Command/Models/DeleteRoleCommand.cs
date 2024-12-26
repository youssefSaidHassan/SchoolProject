using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authorization.Command.Models
{
    public class DeleteRoleCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public DeleteRoleCommand(string id)
        {
            Id = id;
        }
    }
}
