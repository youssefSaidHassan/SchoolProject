using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepository<UserRefreshToken>, IRefreshTokenRepository
    {
        #region Fileds
        private DbSet<UserRefreshToken> userRefreshTokens;
        #endregion

        #region Constructor
        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {
            userRefreshTokens = context.Set<UserRefreshToken>();
        }
        #endregion

        #region Handel Function
        #endregion
    }
}
