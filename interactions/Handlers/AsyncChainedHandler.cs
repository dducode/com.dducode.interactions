using Interactions.Core.Handlers;

namespace Interactions.Handlers;

internal sealed class AsyncChainedHandler<T1, T2, T3>(AsyncHandler<T1, T2> first, AsyncHandler<T2, T3> next) : AsyncHandler<T1, T3> {

  protected internal override async ValueTask<T3> Handle(T1 input, CancellationToken token = default) {
    return await next.Handle(await first.Handle(input, token), token);
  }

}