using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Response;

namespace SchoolProject.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentStudentCountByIdQuery : IRequest<Response<GetDepartmentStudentCountByIdResponse>>
    {
        public int DID { get; set; }
        public GetDepartmentStudentCountByIdQuery(int id)
        {
            DID = id;
        }
    }
}
