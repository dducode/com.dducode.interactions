using Interactions.Actions;
using Interactions.Core.Handlers;

namespace Interactions.Handlers;

internal sealed class AsyncCatchHandler<TException, T1, T2>(
  AsyncHandler<T1, T2> handler,
  AsyncCatch<TException, T1, T2> @catch) : AsyncHandler<T1, T2> where TException : Exception {

  protected internal override async ValueTask<T2> Handle(T1 input, CancellationToken token = default) {
    try {
      return await handler.Handle(input, token);
    }
    catch (TException e) {
      return await @catch(e, input);
    }
  }

}