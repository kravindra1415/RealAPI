namespace WebApplication1.Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }
        IUserRepository UserRepository { get; }
        IPropertyRepository PropertyRepository { get; }


        Task<bool> SaveAsync();
    }
}
