using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Instructors.Commands.Handler
{
    public class InstructorCommandHandler : ResponseHandler,
        IRequestHandler<AddInstructorCommand, Response<string>>
    {
        #region Fileds
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IInstructorService _instructorService;
        #endregion

        #region Constructor
        public InstructorCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            IInstructorService instructorService
            ) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _instructorService = instructorService;
        }


        #endregion

        #region Handel Functions
        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<Instructor>(request);
            var result = await _instructorService.AddInstructorAsync(instructor, request.Image);
            switch (result)
            {
                case "NoFile": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
                case "FailedToUpload": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpload]);
                case "FailedInAdd": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);
            }
            return Success("");
        }

        #endregion
    }
}
