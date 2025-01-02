using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Abstract.Views
{
    public interface IViewRepository<T> : IGenericRepository<T> where T : class
    {
    }
}
