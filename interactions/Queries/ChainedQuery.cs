namespace Interactions.Queries;

internal sealed class ChainedQuery<T1, T2, T3>(Query<T1, T2> first, Query<T2, T3> second) : Query<T1, T3> {

  public override T3 Send(T1 input) {
    return second.Send(first.Send(input));
  }

  public override IDisposable Handle(Handler<T1, T3> handler) {
    throw new InvalidOperationException("Cannot handle chained request");
  }

}

internal sealed class AsyncChainedQuery<T1, T2, T3>(AsyncQuery<T1, T2> first, AsyncQuery<T2, T3> second) : AsyncQuery<T1, T3> {

  public override async ValueTask<T3> Send(T1 input, CancellationToken token = default) {
    return await second.Send(await first.Send(input, token), token);
  }

  public override IDisposable Handle(AsyncHandler<T1, T3> handler) {
    throw new InvalidOperationException("Cannot handle chained request");
  }

}

internal sealed class AsyncProxyQuery<T1, T2>(Query<T1, T2> inner) : AsyncQuery<T1, T2> {

  public override ValueTask<T2> Send(T1 input, CancellationToken token = default) {
    token.ThrowIfCancellationRequested();
    return new ValueTask<T2>(inner.Send(input));
  }

  public override IDisposable Handle(AsyncHandler<T1, T2> handler) {
    throw new InvalidOperationException("Cannot handle proxy request");
  }

}