using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.Departments.Queries.Response
{
    public class GetDepartmentByIdQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MangerName { get; set; }
        public PaginatedResult<StudentResponse>? Students { get; set; }
        public List<InstructorResponse>? Instructors { get; set; }
        public List<SubjectResponse>? Subjects { get; set; }


    }


    public class SubjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class InstructorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StudentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StudentResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
