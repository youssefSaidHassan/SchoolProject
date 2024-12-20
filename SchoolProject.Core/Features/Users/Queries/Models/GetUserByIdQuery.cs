using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Users.Queries.Responses;

namespace SchoolProject.Core.Features.Users.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<UserResponse>>
    {
        public string UserId { get; set; }
        public GetUserByIdQuery(string id)
        {
            UserId = id;
        }
    }
}
