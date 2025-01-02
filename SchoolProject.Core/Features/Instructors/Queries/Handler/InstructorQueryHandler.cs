using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructors.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Instructors.Queries.Handler
{
    public class InstructorQueryHandler : ResponseHandler,
        IRequestHandler<GetSummationSalaryOfInstructorQuery, Response<decimal>>
    {
        #region Fileds
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IInstructorService _instructorService;
        #endregion
        #region Constructor
        public InstructorQueryHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            IInstructorService instructorService
            ) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _instructorService = instructorService;
        }

        #endregion
        #region HandeLFunctions
        public async Task<Response<decimal>> Handle(GetSummationSalaryOfInstructorQuery request, CancellationToken cancellationToken)
        {
            var result = await _instructorService.GetSalarySummationOfInstructor();
            return Success(result);
        }


        #endregion
    }
}
