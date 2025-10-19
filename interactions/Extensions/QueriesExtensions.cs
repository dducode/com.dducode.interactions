using System.Diagnostics.Contracts;
using Interactions.Queries;

namespace Interactions.Extensions;

public static class QueriesExtensions {

  public static T Send<T>(this Query<Unit, T> query) {
    return query.Send(default);
  }

  public static ValueTask<T> Send<T>(this AsyncQuery<Unit, T> query, CancellationToken token = default) {
    return query.Send(default, token);
  }

  [Pure]
  public static AsyncQuery<T1, T2> ToAsyncQuery<T1, T2>(this Query<T1, T2> query) {
    return new AsyncProxyQuery<T1, T2>(query);
  }

  [Pure]
  public static Query<T1, T3> Chain<T1, T2, T3>(this Query<T1, T2> first, Query<T2, T3> second) {
    return new ChainedQuery<T1, T2, T3>(first, second);
  }

  [Pure]
  public static AsyncQuery<T1, T3> Chain<T1, T2, T3>(this AsyncQuery<T1, T2> first, AsyncQuery<T2, T3> second) {
    return new AsyncChainedQuery<T1, T2, T3>(first, second);
  }

  [Pure]
  public static AsyncQuery<T1, T3> Chain<T1, T2, T3>(this AsyncQuery<T1, T2> first, Query<T2, T3> second) {
    return first.Chain(second.ToAsyncQuery());
  }

  [Pure]
  public static AsyncQuery<T1, T3> Chain<T1, T2, T3>(this Query<T1, T2> first, AsyncQuery<T2, T3> second) {
    return first.ToAsyncQuery().Chain(second);
  }

}