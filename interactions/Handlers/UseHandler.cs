namespace Interactions.Handlers;

public delegate T2 Use<T1, T2>(T1 input, Func<T1, T2> next);

public delegate ValueTask<T2> AsyncUse<T1, T2>(T1 input, Func<T1, CancellationToken, ValueTask<T2>> next, CancellationToken token = default);

internal sealed class UseHandler<T1, T2> : Handler<T1, T2> {

  private readonly Use<T1, T2> _func;
  private readonly Handler<T1, T2> _next;

  internal UseHandler(Use<T1, T2> func, Handler<T1, T2> next) {
    _func = func;
    _next = next;
  }

  protected override T2 HandleCore(T1 input) {
    return _func(input, _next.Handle);
  }

}

internal sealed class AsyncUseHandler<T1, T2> : AsyncHandler<T1, T2> {

  private readonly AsyncUse<T1, T2> _func;
  private readonly AsyncHandler<T1, T2> _next;

  internal AsyncUseHandler(AsyncUse<T1, T2> func, AsyncHandler<T1, T2> next) {
    _func = func;
    _next = next;
  }

  protected override ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    return _func(input, _next.Handle, token);
  }

}