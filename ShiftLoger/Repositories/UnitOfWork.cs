using ShiftLoger.Interfaces;

namespace ShiftLoger.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ILogRepository Log { get; set; }

        public UnitOfWork(ILogRepository log)
        {
            Log = log;
        }
    }
}
