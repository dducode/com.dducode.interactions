using System.Diagnostics.Contracts;
using Interactions.Core.Queries;

namespace Interactions.Core.Extensions;

public static class QueriesExtensions {

  public static T Send<T>(this Query<Unit, T> query) {
    return query.Send(default);
  }

  public static Result<T2> TrySend<T1, T2>(this Query<T1, T2> query, T1 input) {
    try {
      return query.Send(input);
    }
    catch (MissingHandlerException e) {
      return e;
    }
  }

  public static ValueTask<T> Send<T>(this AsyncQuery<Unit, T> query, CancellationToken token = default) {
    return query.Send(default, token);
  }

  public static async ValueTask<Result<T2>> TrySend<T1, T2>(this AsyncQuery<T1, T2> query, T1 input, CancellationToken token = default) {
    try {
      return await query.Send(input, token);
    }
    catch (MissingHandlerException e) {
      return e;
    }
    catch (OperationCanceledException e) {
      return e;
    }
  }

  [Pure]
  public static AsyncQuery<T1, T2> ToAsyncQuery<T1, T2>(this Query<T1, T2> query) {
    return new AsyncProxyQuery<T1, T2>(query);
  }

}