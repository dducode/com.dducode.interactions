using Interactions.Core.Handlers;
using Interactions.Core.Queries;

namespace Interactions.Queries;

internal sealed class AsyncChainedQuery<T1, T2, T3>(AsyncQuery<T1, T2> first, AsyncQuery<T2, T3> second) : AsyncQuery<T1, T3> {

  public override async ValueTask<T3> Send(T1 input, CancellationToken token = default) {
    return await second.Send(await first.Send(input, token), token);
  }

  public override IDisposable Handle(AsyncHandler<T1, T3> handler) {
    throw new InvalidOperationException("Cannot handle chained request");
  }

}