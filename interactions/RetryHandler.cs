namespace Interactions;

internal sealed class RetryHandler<T1, T2, TException>(
  AsyncHandler<T1, T2> inner,
  int maxAttempts,
  Func<int, TException, bool> shouldRetry) : AsyncHandler<T1, T2> where TException : Exception {

  private readonly int _maxAttempts = Math.Max(maxAttempts, 1);
  private readonly Func<int, TException, bool> _shouldRetry = shouldRetry ?? delegate { return true; };

  protected override async ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    TException lastEx;
    var attempt = 0;

    do {
      try {
        return await inner.Handle(input, token);
      }
      catch (TException e) {
        lastEx = e;
        attempt++;
      }
    } while (attempt <= _maxAttempts && _shouldRetry(attempt, lastEx));

    throw lastEx;
  }

}