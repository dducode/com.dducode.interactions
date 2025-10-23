using Interactions.Core;

namespace Interactions;

internal sealed class RetryHandler<T1, T2, TException>(
  AsyncHandler<T1, T2> inner,
  Func<int, TException, CancellationToken, ValueTask<bool>> shouldRetry) : AsyncHandler<T1, T2> where TException : Exception {

  protected internal override async ValueTask<T2> Handle(T1 input, CancellationToken token = default) {
    var attempt = 0;

    do {
      try {
        return await inner.Handle(input, token);
      }
      catch (TException e) {
        if (!await shouldRetry(++attempt, e, token))
          throw;
      }
    } while (true);
  }

}