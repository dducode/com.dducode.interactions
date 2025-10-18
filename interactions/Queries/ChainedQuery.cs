namespace Interactions.Queries;

internal sealed class ChainedQuery<TIn, TOut, TResponse> : Query<TIn, TResponse> {

  private readonly Query<TIn, TOut> _first;
  private readonly Query<TOut, TResponse> _second;

  internal ChainedQuery(Query<TIn, TOut> first, Query<TOut, TResponse> second) {
    _first = first;
    _second = second;
  }

  public override TResponse Send(TIn input) {
    return _second.Send(_first.Send(input));
  }

  public override IDisposable Handle(Handler<TIn, TResponse> handler) {
    throw new InvalidOperationException("Cannot handle chained request");
  }

}

internal sealed class AsyncChainedQuery<TIn, TOut, TResponse> : AsyncQuery<TIn, TResponse> {

  private readonly AsyncQuery<TIn, TOut> _first;
  private readonly AsyncQuery<TOut, TResponse> _second;

  internal AsyncChainedQuery(AsyncQuery<TIn, TOut> first, AsyncQuery<TOut, TResponse> second) {
    _first = first;
    _second = second;
  }

  public override async ValueTask<TResponse> Send(TIn input, CancellationToken token = default) {
    return await _second.Send(await _first.Send(input, token), token);
  }

  public override IDisposable Handle(AsyncHandler<TIn, TResponse> handler) {
    throw new InvalidOperationException("Cannot handle chained request");
  }

}

internal sealed class AsyncProxyQuery<TIn, TResponse> : AsyncQuery<TIn, TResponse> {

  private readonly Query<TIn, TResponse> _query;

  internal AsyncProxyQuery(Query<TIn, TResponse> query) {
    _query = query;
  }

  public override ValueTask<TResponse> Send(TIn input, CancellationToken token = default) {
    token.ThrowIfCancellationRequested();
    return new ValueTask<TResponse>(_query.Send(input));
  }

  public override IDisposable Handle(AsyncHandler<TIn, TResponse> handler) {
    throw new InvalidOperationException("Cannot handle proxy request");
  }

}