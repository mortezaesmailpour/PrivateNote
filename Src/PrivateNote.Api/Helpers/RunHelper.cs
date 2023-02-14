using System.Runtime.CompilerServices;

namespace PrivateNote.Api.Helpers;

public static class RunHelper
{
    public static async Task<TResult?> TryRunWithTimeLog<TResult>(Func<Task<TResult>> action,ILogger logger,
        [CallerArgumentExpression("action")] string message = "") where TResult : class?
    {
        logger.LogInformation("{0} is starting ...", message);
        Stopwatch stopWatch = new();
        stopWatch.Start();
        try
        {
            var result = await action();
            stopWatch.Stop();
            logger.LogInformation("{0} finished in {1}ms", message, stopWatch.ElapsedMilliseconds);
            return result;
        }
        catch (Exception e)
        {
            stopWatch.Stop();
            logger.LogError(e, "something went wrong while running {0} in {1}ms ", message,
                stopWatch.ElapsedMilliseconds);
            return null;
        }
    }
}