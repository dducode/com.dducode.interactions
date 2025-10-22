namespace Interactions.Handlers;

public delegate T2 Use<in T1, out T2, out T3, in T4>(T1 input, Func<T3, T4> next);

public delegate ValueTask<T2> AsyncUse<in T1, T2, out T3, T4>(
  T1 input, Func<T3, CancellationToken, ValueTask<T4>> next, CancellationToken token = default
);

internal sealed class UseHandler<T1, T2, T3, T4>(Use<T1, T2, T3, T4> func, Handler<T3, T4> next) : Handler<T1, T2> {

  protected override T2 HandleCore(T1 input) {
    return func(input, next.Handle);
  }

}

internal sealed class AsyncUseHandler<T1, T2, T3, T4>(AsyncUse<T1, T2, T3, T4> func, AsyncHandler<T3, T4> next) : AsyncHandler<T1, T2> {

  protected override ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    return func(input, next.Handle, token);
  }

}