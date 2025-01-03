using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Users.Commands.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public DeleteUserCommand(string id)
        {
            UserId = id;
        }
    }
}
