namespace Interactions.Handlers;

internal sealed class ChainedHandler<T1, T2, T3> : Handler<T1, T3> {

  private readonly Handler<T1, T2> _first;
  private readonly Handler<T2, T3> _next;

  internal ChainedHandler(Handler<T1, T2> first, Handler<T2, T3> next) {
    _first = first;
    _next = next;
  }

  protected override T3 HandleCore(T1 input) {
    return _next.Handle(_first.Handle(input));
  }

}

internal sealed class AsyncChainedHandler<T1, T2, T3> : AsyncHandler<T1, T3> {

  private readonly AsyncHandler<T1, T2> _first;
  private readonly AsyncHandler<T2, T3> _next;

  internal AsyncChainedHandler(AsyncHandler<T1, T2> first, AsyncHandler<T2, T3> next) {
    _first = first;
    _next = next;
  }

  protected override async ValueTask<T3> HandleCore(T1 input, CancellationToken token = default) {
    return await _next.Handle(await _first.Handle(input, token), token);
  }

}