using MediatR;
using SchoolProject.Core.Features.Users.Queries.Responses;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.Users.Queries.Models
{
    public class GetUserPaginatedQuery : IRequest<PaginatedResult<UserResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
