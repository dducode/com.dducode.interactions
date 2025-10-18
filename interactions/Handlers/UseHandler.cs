namespace Interactions.Handlers;

public delegate TOut Use<TIn, TOut>(TIn input, Func<TIn, TOut> next);

public delegate ValueTask<TOut> AsyncUse<TIn, TOut>(
  TIn input, Func<TIn, CancellationToken, ValueTask<TOut>> next, CancellationToken token = default
);

internal sealed class UseHandler<TIn, TOut> : Handler<TIn, TOut> {

  private readonly Use<TIn, TOut> _func;
  private readonly Handler<TIn, TOut> _next;

  internal UseHandler(Use<TIn, TOut> func, Handler<TIn, TOut> next) {
    _func = func;
    _next = next;
  }

  protected override TOut HandleCore(TIn input) {
    return _func(input, _next.Handle);
  }

}

internal sealed class AsyncUseHandler<TIn, TOut> : AsyncHandler<TIn, TOut> {

  private readonly AsyncUse<TIn, TOut> _func;
  private readonly AsyncHandler<TIn, TOut> _next;

  internal AsyncUseHandler(AsyncUse<TIn, TOut> func, AsyncHandler<TIn, TOut> next) {
    _func = func;
    _next = next;
  }

  protected override ValueTask<TOut> HandleCore(TIn input, CancellationToken token = default) {
    return _func(input, _next.Handle, token);
  }

}