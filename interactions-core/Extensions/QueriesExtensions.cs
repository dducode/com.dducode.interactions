using System.Diagnostics.Contracts;
using Interactions.Core.Queries;

namespace Interactions.Core.Extensions;

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

}