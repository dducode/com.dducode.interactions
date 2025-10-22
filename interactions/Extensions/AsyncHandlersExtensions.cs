using System.Diagnostics.Contracts;
using Interactions.Actions;
using Interactions.Analytics;
using Interactions.Core.Extensions;
using Interactions.Core.Handlers;
using Interactions.Handlers;
using Interactions.Transformation;

namespace Interactions.Extensions;

public static class AsyncHandlersExtensions {

  [Pure]
  public static AsyncHandler<T1, T2> Catch<TException, T1, T2>(
    this AsyncHandler<T1, T2> handler, AsyncCatch<TException, T1, T2> @catch) where TException : Exception {
    return new AsyncCatchHandler<TException, T1, T2>(handler, @catch);
  }

  [Pure]
  public static AsyncHandler<T1, T2> Catch<TException, T1, T2>(
    this AsyncHandler<T1, T2> handler, Catch<TException, T1, T2> @catch) where TException : Exception {
    return handler.Catch<TException, T1, T2>((exception, input) => new ValueTask<T2>(@catch(exception, input)));
  }

  [Pure]
  public static AsyncHandler<T1, T2> Finally<T1, T2>(this AsyncHandler<T1, T2> handler, AsyncFinally<T1> @finally) {
    return new AsyncFinallyHandler<T1, T2>(handler, @finally);
  }

  [Pure]
  public static AsyncHandler<T1, T2> Finally<T1, T2>(this AsyncHandler<T1, T2> handler, Finally<T1> @finally) {
    return handler.Finally(input => {
      @finally(input);
      return new ValueTask();
    });
  }

  [Pure]
  public static AsyncHandler<T1, T3> Next<T1, T2, T3>(this AsyncHandler<T1, T2> handler, AsyncHandler<T2, T3> nextHandler) {
    return new AsyncChainedHandler<T1, T2, T3>(handler, nextHandler);
  }

  [Pure]
  public static AsyncHandler<T1, T3> Next<T1, T2, T3>(this AsyncHandler<T1, T2> handler, Handler<T2, T3> nextHandler) {
    return new AsyncChainedHandler<T1, T2, T3>(handler, nextHandler.ToAsyncHandler());
  }

  [Pure]
  public static AsyncHandler<T1, T3> Next<T1, T2, T3>(this AsyncHandler<T1, T2> handler, Func<T2, CancellationToken, ValueTask<T3>> nextHandler) {
    return handler.Next(AsyncHandler.FromMethod(nextHandler));
  }

  [Pure]
  public static AsyncHandler<T1, T3> Next<T1, T2, T3>(this AsyncHandler<T1, T2> handler, Func<T2, T3> nextHandler) {
    return handler.Next(Handler.FromMethod(nextHandler));
  }

  [Pure]
  public static AsyncHandler<T1, T2> Retry<T1, T2, TException>(
    this AsyncHandler<T1, T2> handler, Func<int, TException, bool> shouldRetry, Func<int, TimeSpan> backoff = null) where TException : Exception {
    return new RetryHandler<T1, T2, TException>(handler, shouldRetry, backoff);
  }

  [Pure]
  public static AsyncHandler<K1, K2> Transform<T1, T2, K1, K2>(
    this AsyncHandler<T1, T2> handler, Transformer<K1, T1> incoming, Transformer<T2, K2> outgoing) {
    return new AsyncTransformHandler<K1, T1, T2, K2>(incoming, handler, outgoing);
  }

  [Pure]
  public static AsyncHandler<K1, K2> Transform<T1, T2, K1, K2>(this AsyncHandler<T1, T2> handler, Func<K1, T1> incoming, Func<T2, K2> outgoing) {
    return handler.Transform(Transformer.FromMethod(incoming), Transformer.FromMethod(outgoing));
  }

  [Pure]
  public static AsyncHandler<K1, T2> InputTransform<T1, T2, K1>(this AsyncHandler<T1, T2> handler, Transformer<K1, T1> incoming) {
    return handler.Transform(incoming, Transformer.Identity<T2>());
  }

  [Pure]
  public static AsyncHandler<T1, K2> OutputTransform<T1, T2, K2>(this AsyncHandler<T1, T2> handler, Transformer<T2, K2> outgoing) {
    return handler.Transform(Transformer.Identity<T1>(), outgoing);
  }

  [Pure]
  public static AsyncHandler<K1, T2> InputTransform<T1, T2, K1>(this AsyncHandler<T1, T2> handler, Func<K1, T1> incoming) {
    return handler.InputTransform(Transformer.FromMethod(incoming));
  }

  [Pure]
  public static AsyncHandler<T1, K2> OutputTransform<T1, T2, K2>(this AsyncHandler<T1, T2> handler, Func<T2, K2> outgoing) {
    return handler.OutputTransform(Transformer.FromMethod(outgoing));
  }

  [Pure]
  public static AsyncHandler<T1, T2> Do<T1, T2>(this AsyncHandler<T1, T2> handler, AsyncSideAction<T2> action) {
    return handler.Next(new AsyncTransitiveHandler<T2>(action));
  }

  [Pure]
  public static AsyncHandler<T1, T2> Do<T1, T2>(this AsyncHandler<T1, T2> handler, SideAction<T2> action) {
    return handler.Next(new TransitiveHandler<T2>(action));
  }

  [Pure]
  public static AsyncHandler<T1, T2> Delay<T1, T2>(this AsyncHandler<T1, T2> handler, Func<T2, TimeSpan> timeDelay) {
    return handler.Next(new DelayHandler<T2>(timeDelay));
  }

  [Pure]
  public static AsyncHandler<T1, T2> Delay<T1, T2>(this AsyncHandler<T1, T2> handler, TimeSpan timeDelay) {
    return handler.Next(new DelayHandler<T2>(delegate { return timeDelay; }));
  }

  [Pure]
  public static AsyncHandler<T1, T2> Metrics<T1, T2>(this AsyncHandler<T1, T2> handler, IMetrics<T1, T2> metrics, string tag = null) {
    return new AsyncMetricsHandler<T1, T2>(handler, metrics, tag);
  }

}