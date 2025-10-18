using System.Diagnostics.Contracts;
using Interactions.Queries;

namespace Interactions.Extensions;

public static class QueriesExtensions {

  public static TOut Send<TOut>(this Query<Unit, TOut> query) {
    return query.Send(default);
  }

  public static ValueTask<TOut> Send<TOut>(this AsyncQuery<Unit, TOut> query, CancellationToken token = default) {
    return query.Send(default, token);
  }

  [Pure]
  public static AsyncQuery<TIn, TResponse> ToAsyncQuery<TIn, TResponse>(this Query<TIn, TResponse> query) {
    return new AsyncProxyQuery<TIn, TResponse>(query);
  }

  [Pure]
  public static Query<TIn, TResponse> Chain<TIn, TOut, TResponse>(this Query<TIn, TOut> first, Query<TOut, TResponse> second) {
    return new ChainedQuery<TIn, TOut, TResponse>(first, second);
  }

  [Pure]
  public static AsyncQuery<TIn, TResponse> Chain<TIn, TOut, TResponse>(this AsyncQuery<TIn, TOut> first, AsyncQuery<TOut, TResponse> second) {
    return new AsyncChainedQuery<TIn, TOut, TResponse>(first, second);
  }

  [Pure]
  public static AsyncQuery<TIn, TResponse> Chain<TIn, TOut, TResponse>(this AsyncQuery<TIn, TOut> first, Query<TOut, TResponse> second) {
    return first.Chain(second.ToAsyncQuery());
  }

  [Pure]
  public static AsyncQuery<TIn, TResponse> Chain<TIn, TOut, TResponse>(this Query<TIn, TOut> first, AsyncQuery<TOut, TResponse> second) {
    return first.ToAsyncQuery().Chain(second);
  }

}