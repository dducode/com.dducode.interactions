namespace Interactions.Handlers;

internal sealed class AsyncProxyHandler<TIn, TOut> : AsyncHandler<TIn, TOut> {

  private readonly Handler<TIn, TOut> _handler;

  internal AsyncProxyHandler(Handler<TIn, TOut> handler) {
    _handler = handler;
  }

  protected override ValueTask<TOut> HandleCore(TIn input, CancellationToken token = default) {
    token.ThrowIfCancellationRequested();
    return new ValueTask<TOut>(_handler.Handle(input));
  }

}