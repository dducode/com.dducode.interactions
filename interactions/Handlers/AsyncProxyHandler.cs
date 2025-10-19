namespace Interactions.Handlers;

internal sealed class AsyncProxyHandler<T1, T2> : AsyncHandler<T1, T2> {

  private readonly Handler<T1, T2> _handler;

  internal AsyncProxyHandler(Handler<T1, T2> handler) {
    _handler = handler;
  }

  protected override ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    token.ThrowIfCancellationRequested();
    return new ValueTask<T2>(_handler.Handle(input));
  }

}