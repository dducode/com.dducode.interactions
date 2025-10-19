namespace Interactions.Handlers;

public delegate T2 Use<T1, T2>(T1 input, Func<T1, T2> next);

public delegate ValueTask<T2> AsyncUse<T1, T2>(T1 input, Func<T1, CancellationToken, ValueTask<T2>> next, CancellationToken token = default);

internal sealed class UseHandler<T1, T2>(Use<T1, T2> func, Handler<T1, T2> next) : Handler<T1, T2> {

  protected override T2 HandleCore(T1 input) {
    return func(input, next.Handle);
  }

}

internal sealed class AsyncUseHandler<T1, T2>(AsyncUse<T1, T2> func, AsyncHandler<T1, T2> next) : AsyncHandler<T1, T2> {

  protected override ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    return func(input, next.Handle, token);
  }

}