using Interactions.Core;

namespace Interactions.Handlers;

internal sealed class AsyncFinallyHandler<T1, T2>(AsyncHandler<T1, T2> handler, AsyncAction<T1> @finally) : AsyncHandler<T1, T2> {

  public override async ValueTask<T2> Handle(T1 input, CancellationToken token = default) {
    try {
      return await handler.Handle(input, token);
    }
    finally {
      await @finally(input, token);
    }
  }

}