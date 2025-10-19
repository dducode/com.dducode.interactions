using System.Diagnostics.Contracts;

namespace Interactions.Extensions;

public static class HandleableExtensions {

  [Pure]
  public static Handleable<T1, T2> Merge<T1, T2>(this Handleable<T1, T2> first, Handleable<T1, T2> second) {
    return new MergedHandleable<T1, T2>(first, second);
  }

  [Pure]
  public static AsyncHandleable<TIn, TOut> Merge<TIn, TOut>(this AsyncHandleable<TIn, TOut> first, AsyncHandleable<TIn, TOut> second) {
    return new AsyncMergedHandleable<TIn, TOut>(first, second);
  }

}