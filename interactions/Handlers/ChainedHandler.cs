namespace Interactions.Handlers;

internal sealed class ChainedHandler<T1, T2, T3>(Handler<T1, T2> first, Handler<T2, T3> next) : Handler<T1, T3> {

  protected override T3 HandleCore(T1 input) {
    return next.Handle(first.Handle(input));
  }

}

internal sealed class AsyncChainedHandler<T1, T2, T3>(AsyncHandler<T1, T2> first, AsyncHandler<T2, T3> next) : AsyncHandler<T1, T3> {

  protected override async ValueTask<T3> HandleCore(T1 input, CancellationToken token = default) {
    return await next.Handle(await first.Handle(input, token), token);
  }

}