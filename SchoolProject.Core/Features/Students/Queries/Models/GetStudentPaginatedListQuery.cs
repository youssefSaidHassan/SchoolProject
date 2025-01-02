using MediatR;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Enums;

namespace SchoolProject.Core.Features.Students.Queries.Models
{
    public class GetStudentPaginatedListQuery : IRequest<PaginatedResult<GetStudentPaginatedListResponse>>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public StudentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }

        public GetStudentPaginatedListQuery()
        {

        }
    }
}
