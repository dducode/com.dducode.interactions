using Interactions.Core;

namespace Interactions.Handlers;

internal sealed class AsyncCompositeHandler<T1, T2, T3>(AsyncHandler<T1, T2> first, AsyncHandler<T2, T3> second) : AsyncHandler<T1, T3> {

  protected internal override async ValueTask<T3> Handle(T1 input, CancellationToken token = default) {
    return await second.Handle(await first.Handle(input, token), token);
  }

}