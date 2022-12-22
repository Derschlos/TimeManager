using TimeManagerMVC.Interfaces;

namespace TimeManagerMVC.Reporitories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ILogApiRepository LogApi { get; }
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }

        public UnitOfWork(ILogApiRepository LogApi, IUserRepository Users, IRoleRepository Roles)
        {
            this.LogApi = LogApi;
            this.Users = Users;
            this.Roles = Roles;
        }
    }
}
