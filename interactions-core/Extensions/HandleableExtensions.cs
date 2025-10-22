using System.Diagnostics.Contracts;
using Interactions.Core.Handlers;

namespace Interactions.Core.Extensions;

public static class HandleableExtensions {

  [Pure]
  public static Handleable<T1, T2> Merge<T1, T2>(this Handleable<T1, T2> first, Handleable<T1, T2> second) {
    return new MergedHandleable<T1, T2>(first, second);
  }

  [Pure]
  public static AsyncHandleable<T1, T2> Merge<T1, T2>(this AsyncHandleable<T1, T2> first, AsyncHandleable<T1, T2> second) {
    return new AsyncMergedHandleable<T1, T2>(first, second);
  }

  public static IDisposable Handle<T1, T2>(this Handleable<T1, T2> handleable, Func<T1, T2> handler) {
    return handleable.Handle(new AnonymousHandler<T1, T2>(handler));
  }

  public static IDisposable Handle<T>(this Handleable<Unit, T> handleable, Func<T> handler) {
    return handleable.Handle(new AnonymousHandler<Unit, T>(delegate {
      return handler();
    }));
  }

  public static IDisposable Handle<T>(this Handleable<T, Unit> handleable, Action<T> handler) {
    return handleable.Handle(new AnonymousHandler<T>(handler));
  }

  public static IDisposable Handle(this Handleable<Unit, Unit> handleable, Action handler) {
    return handleable.Handle(new AnonymousHandler(handler));
  }

  public static IDisposable Handle<T1, T2>(this AsyncHandleable<T1, T2> handleable, Func<T1, CancellationToken, ValueTask<T2>> handler) {
    return handleable.Handle(new AsyncAnonymousHandler<T1, T2>(handler));
  }

  public static IDisposable Handle<T>(this AsyncHandleable<Unit, T> handleable, Func<CancellationToken, ValueTask<T>> handler) {
    return handleable.Handle(new AsyncAnonymousHandler<Unit, T>((_, token) => handler(token)));
  }

  public static IDisposable Handle<T>(this AsyncHandleable<T, Unit> handleable, Func<T, CancellationToken, ValueTask> handler) {
    return handleable.Handle(new AsyncAnonymousHandler<T>(handler));
  }

  public static IDisposable Handle(this AsyncHandleable<Unit, Unit> handleable, Func<CancellationToken, ValueTask> handler) {
    return handleable.Handle(new AsyncAnonymousHandler(handler));
  }

}