namespace Interactions.Queries;

internal sealed class ChainedQuery<T1, T2, T3> : Query<T1, T3> {

  private readonly Query<T1, T2> _first;
  private readonly Query<T2, T3> _second;

  internal ChainedQuery(Query<T1, T2> first, Query<T2, T3> second) {
    _first = first;
    _second = second;
  }

  public override T3 Send(T1 input) {
    return _second.Send(_first.Send(input));
  }

  public override IDisposable Handle(Handler<T1, T3> handler) {
    throw new InvalidOperationException("Cannot handle chained request");
  }

}

internal sealed class AsyncChainedQuery<T1, T2, T3> : AsyncQuery<T1, T3> {

  private readonly AsyncQuery<T1, T2> _first;
  private readonly AsyncQuery<T2, T3> _second;

  internal AsyncChainedQuery(AsyncQuery<T1, T2> first, AsyncQuery<T2, T3> second) {
    _first = first;
    _second = second;
  }

  public override async ValueTask<T3> Send(T1 input, CancellationToken token = default) {
    return await _second.Send(await _first.Send(input, token), token);
  }

  public override IDisposable Handle(AsyncHandler<T1, T3> handler) {
    throw new InvalidOperationException("Cannot handle chained request");
  }

}

internal sealed class AsyncProxyQuery<T1, T2> : AsyncQuery<T1, T2> {

  private readonly Query<T1, T2> _query;

  internal AsyncProxyQuery(Query<T1, T2> query) {
    _query = query;
  }

  public override ValueTask<T2> Send(T1 input, CancellationToken token = default) {
    token.ThrowIfCancellationRequested();
    return new ValueTask<T2>(_query.Send(input));
  }

  public override IDisposable Handle(AsyncHandler<T1, T2> handler) {
    throw new InvalidOperationException("Cannot handle proxy request");
  }

}