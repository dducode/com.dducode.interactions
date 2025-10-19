namespace Interactions;

internal sealed class RetryHandler<T1, T2, TException>(
  AsyncHandler<T1, T2> inner,
  Func<int, TException, bool> shouldRetry,
  Func<int, TimeSpan> backoff = null) : AsyncHandler<T1, T2> where TException : Exception {

  private readonly Func<int, TimeSpan> _backoff = backoff ?? delegate { return TimeSpan.Zero; };

  protected override async ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    var attempt = 0;

    do {
      try {
        return await inner.Handle(input, token);
      }
      catch (TException e) {
        if (!shouldRetry(++attempt, e))
          throw;
        await Task.Delay(_backoff(attempt), token);
      }
    } while (true);
  }

}