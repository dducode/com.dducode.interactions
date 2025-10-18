namespace Interactions.Extensions;

public static class HandleableExtensions {

  public static Handleable<TIn, TOut> Merge<TIn, TOut>(this Handleable<TIn, TOut> first, Handleable<TIn, TOut> second) {
    return new MergedHandleable<TIn, TOut>(first, second);
  }

  public static AsyncHandleable<TIn, TOut> Merge<TIn, TOut>(this AsyncHandleable<TIn, TOut> first, AsyncHandleable<TIn, TOut> second) {
    return new AsyncMergedHandleable<TIn, TOut>(first, second);
  }

}