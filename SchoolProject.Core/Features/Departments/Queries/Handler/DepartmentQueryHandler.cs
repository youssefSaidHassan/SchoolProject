using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Departments.Queries.Response;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Service.Abstracts;
using Serilog;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Departments.Queries.Handler
{
    public class DepartmentQueryHandler : ResponseHandler,
        IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdQueryResponse>>,
        IRequestHandler<GetDepartmentStudentListCountQuery, Response<List<GetDepartmentStudentListCountResponse>>>,
        IRequestHandler<GetDepartmentStudentCountByIdQuery, Response<GetDepartmentStudentCountByIdResponse>>
    {
        #region Fileds
        private readonly IDepartmentServices _departmentServices;
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructor(s)
        public DepartmentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IDepartmentServices departmentServices,
            IMapper mapper,
            IStudentService studentService) : base(stringLocalizer)
        {
            this._departmentServices = departmentServices;
            this._mapper = mapper;
            this._studentService = studentService;
            this._stringLocalizer = stringLocalizer;
        }





        #endregion

        #region Handel Functions


        async Task<Response<GetDepartmentByIdQueryResponse>> IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdQueryResponse>>.Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            // Call Services
            var department = await _departmentServices.GetDepartmentById(request.Id);
            // check if this is exist
            if (department == null)
            {
                return NotFound<GetDepartmentByIdQueryResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }
            // mapping
            var departmentMapping = _mapper.Map<GetDepartmentByIdQueryResponse>(department);

            // pagination 

            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.Id, e.Localize(e.NameAr, e.NameEn));
            var studentQurable = _studentService.GetStudentsQuerableByDepartmentId(request.Id);
            var paginatedQurable = await studentQurable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            departmentMapping.Students = paginatedQurable;
            Log.Information($"Get Department By Id {request.Id}!");
            // return response
            return Success(departmentMapping);
        }

        public async Task<Response<List<GetDepartmentStudentListCountResponse>>> Handle(GetDepartmentStudentListCountQuery request, CancellationToken cancellationToken)
        {
            var result = await _departmentServices.GetViewDepartmentDataAsync();
            var resultMapping = _mapper.Map<List<GetDepartmentStudentListCountResponse>>(result);
            return Success(resultMapping);
        }

        public async Task<Response<GetDepartmentStudentCountByIdResponse>> Handle(GetDepartmentStudentCountByIdQuery request, CancellationToken cancellationToken)
        {
            var parameter = _mapper.Map<DepartmentStudentCountProcParameters>(request);
            var result = await _departmentServices.GetDepartmentStudentCountProcs(parameter);
            var resultMapping = _mapper.Map<GetDepartmentStudentCountByIdResponse>(result.FirstOrDefault());
            return Success(resultMapping);

        }

        #endregion
    }
}
