using TimeManagerMVC.Interfaces;

namespace TimeManagerMVC.Reporitories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ILogApiRepository LogApi { get; }
        public UnitOfWork(ILogApiRepository LogApi)
        {
            this.LogApi = LogApi;
        }
    }
}
