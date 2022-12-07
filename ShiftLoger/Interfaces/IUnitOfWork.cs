namespace ShiftLoger.Interfaces
{
    public interface IUnitOfWork
    {
        ILogRepository Log { get; }
    }
}
