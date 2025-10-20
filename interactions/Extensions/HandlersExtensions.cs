using System.Diagnostics.Contracts;
using Interactions.Analytics;
using Interactions.Handlers;

namespace Interactions.Extensions;

public static class HandlersExtensions {

  [Pure]
  public static AsyncHandler<T1, T2> ToAsyncHandler<T1, T2>(this Handler<T1, T2> handler) {
    return new AsyncProxyHandler<T1, T2>(handler);
  }

  [Pure]
  public static Handler<T1, T3> Next<T1, T2, T3>(this Handler<T1, T2> handler, Handler<T2, T3> nextHandler) {
    return new ChainedHandler<T1, T2, T3>(handler, nextHandler);
  }

  [Pure]
  public static AsyncHandler<T1, T3> Next<T1, T2, T3>(this Handler<T1, T2> handler, AsyncHandler<T2, T3> nextHandler) {
    return new AsyncChainedHandler<T1, T2, T3>(handler.ToAsyncHandler(), nextHandler);
  }

  [Pure]
  public static Handler<T1, T3> Next<T1, T2, T3>(this Handler<T1, T2> handler, Func<T2, T3> nextHandler) {
    return handler.Next(Handler.FromMethod(nextHandler));
  }

  [Pure]
  public static AsyncHandler<T1, T3> Next<T1, T2, T3>(this Handler<T1, T2> handler, Func<T2, CancellationToken, ValueTask<T3>> nextHandler) {
    return handler.Next(AsyncHandler.FromMethod(nextHandler));
  }

  [Pure]
  public static Handler<T1, T2> Do<T1, T2>(this Handler<T1, T2> handler, SideAction<T2> action) {
    return handler.Next(new TransitiveHandler<T2>(action));
  }

  [Pure]
  public static AsyncHandler<T1, T2> Do<T1, T2>(this Handler<T1, T2> handler, AsyncSideAction<T2> action) {
    return handler.Next(new AsyncTransitiveHandler<T2>(action));
  }

  [Pure]
  public static AsyncHandler<T1, T2> Delay<T1, T2>(this Handler<T1, T2> handler, Func<T2, TimeSpan> timeDelay) {
    return handler.Next(new DelayHandler<T2>(timeDelay));
  }

  [Pure]
  public static AsyncHandler<T1, T2> Delay<T1, T2>(this Handler<T1, T2> handler, TimeSpan timeDelay) {
    return handler.Next(new DelayHandler<T2>(delegate { return timeDelay; }));
  }

  [Pure]
  public static Handler<T1, T2> Metrics<T1, T2>(this Handler<T1, T2> handler, IMetrics<T1, T2> metrics, string tag = null) {
    return new MetricsHandler<T1, T2>(handler, metrics, tag);
  }

}