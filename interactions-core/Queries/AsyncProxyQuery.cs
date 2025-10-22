using Interactions.Core.Handlers;

namespace Interactions.Core.Queries;

internal sealed class AsyncProxyQuery<T1, T2>(Query<T1, T2> inner) : AsyncQuery<T1, T2> {

  public override ValueTask<T2> Send(T1 input, CancellationToken token = default) {
    token.ThrowIfCancellationRequested();
    return new ValueTask<T2>(inner.Send(input));
  }

  public override IDisposable Handle(AsyncHandler<T1, T2> handler) {
    throw new InvalidOperationException("Cannot handle proxy request");
  }

}