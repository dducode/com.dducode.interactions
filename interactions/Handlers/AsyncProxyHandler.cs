namespace Interactions.Handlers;

internal sealed class AsyncProxyHandler<T1, T2>(Handler<T1, T2> handler) : AsyncHandler<T1, T2> {

  protected override ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    token.ThrowIfCancellationRequested();
    return new ValueTask<T2>(handler.Handle(input));
  }

}