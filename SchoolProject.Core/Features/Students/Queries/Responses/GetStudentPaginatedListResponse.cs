namespace SchoolProject.Core.Features.Students.Queries.Responses
{
    public class GetStudentPaginatedListResponse
    {
        public GetStudentPaginatedListResponse()
        {

        }
        public GetStudentPaginatedListResponse(int id, string name, string address, string departmentName)
        {
            Id = id;
            Name = name;
            Address = address;
            DepartmentName = departmentName;
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public string DepartmentName { get; set; }
    }
}
