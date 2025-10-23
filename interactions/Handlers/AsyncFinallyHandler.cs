using Interactions.Actions;
using Interactions.Core;

namespace Interactions.Handlers;

internal sealed class AsyncFinallyHandler<T1, T2>(AsyncHandler<T1, T2> handler, AsyncFinally<T1> @finally) : AsyncHandler<T1, T2> {

  protected internal override async ValueTask<T2> Handle(T1 input, CancellationToken token = default) {
    try {
      return await handler.Handle(input, token);
    }
    finally {
      await @finally(input);
    }
  }

}