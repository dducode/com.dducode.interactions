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
    return handleable.Handle(new AnonymousHandler_Func<T1, T2>(handler));
  }

  public static IDisposable Handle<T>(this Handleable<Unit, T> handleable, Func<T> handler) {
    return handleable.Handle(new AnonymousHandler_Func<T>(handler));
  }

  public static IDisposable Handle<T>(this Handleable<T, Unit> handleable, Action<T> handler) {
    return handleable.Handle(new AnonymousHandler_Action<T>(handler));
  }

  public static IDisposable Handle(this Handleable<Unit, Unit> handleable, Action handler) {
    return handleable.Handle(new AnonymousHandler_Action(handler));
  }

  public static IDisposable Handle<T1, T2>(this AsyncHandleable<T1, T2> handleable, AsyncFunc<T1, T2> handler) {
    return handleable.Handle(new AsyncAnonymousHandler_Func<T1, T2>(handler));
  }

  public static IDisposable Handle<T>(this AsyncHandleable<Unit, T> handleable, AsyncFunc<T> handler) {
    return handleable.Handle(new AsyncAnonymousHandler_Func<T>(handler));
  }

  public static IDisposable Handle<T>(this AsyncHandleable<T, Unit> handleable, AsyncAction<T> handler) {
    return handleable.Handle(new AsyncAnonymousHandler_Action<T>(handler));
  }

  public static IDisposable Handle(this AsyncHandleable<Unit, Unit> handleable, AsyncAction handler) {
    return handleable.Handle(new AsyncAnonymousHandler_Action(handler));
  }

}