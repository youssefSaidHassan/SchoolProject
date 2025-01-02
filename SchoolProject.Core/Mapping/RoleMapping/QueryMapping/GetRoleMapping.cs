using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Features.Authorization.Query.Responses;

namespace SchoolProject.Core.Mapping.RoleMapping
{
    public partial class RoleProfile
    {
        public void GetRoleMapping()
        {
            CreateMap<IdentityRole, GetRoleResponse>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
