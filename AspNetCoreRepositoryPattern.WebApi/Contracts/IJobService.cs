namespace AspNetCoreRepositoryPattern.Contracts;

/* For Hangfire */
public interface IJobService
{
    void FireAndForgetJob();
    void RecurringJob();
    void DelayedJob();
    void ContinuationJob();
}