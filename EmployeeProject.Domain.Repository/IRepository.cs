namespace EmployeeProject.Domain.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
    }
}