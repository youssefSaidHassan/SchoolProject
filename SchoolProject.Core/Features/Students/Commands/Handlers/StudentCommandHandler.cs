using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>, IRequestHandler<EditStudentCommand, Response<string>>,
         IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructor
        public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            this._studentService = studentService;
            this._mapper = mapper;
            this._stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Handel Methods

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapper = _mapper.Map<Student>(request);
            var result = await _studentService.CreateAsync(studentMapper);
            if (result == "Success") return Created("");
            else return BadRequest<string>();

        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            // check if the id is exist
            var student = await _studentService.GetStudentByIDAsync(request.Id);
            if (student == null) return NotFound<string>("Student Is Not Found");
            // mapping
            var studentMapper = _mapper.Map(request, student);
            // call service
            var result = await _studentService.EditAsync(studentMapper);
            // return response
            if (result == "Success") return Success($"Edit Successfully {studentMapper.Id}");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            // check student
            var student = await _studentService.GetStudentByIDWithoutIncludingAsync(request.Id);
            if (student == null) return NotFound<string>("Student Not Found");

            // call service

            var result = await _studentService.DeleteAsync(student);

            if (result == "Success") return Deleted<string>();
            else return BadRequest<string>();

        }
        #endregion
    }
}
