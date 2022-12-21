namespace TimeManagerMVC.Interfaces
{
    public interface IUnitOfWork
    {
        ILogApiRepository LogApi { get; }
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }

    }
}
