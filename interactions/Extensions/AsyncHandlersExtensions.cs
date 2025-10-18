using System.Diagnostics.Contracts;
using Interactions.Handlers;
using Interactions.Transformation;

namespace Interactions.Extensions;

public static class AsyncHandlersExtensions {

  [Pure]
  public static AsyncHandler<TIn, TNext> Next<TIn, TCurrent, TNext>(
    this AsyncHandler<TIn, TCurrent> handler, AsyncHandler<TCurrent, TNext> nextHandler) {
    return new AsyncChainedHandler<TIn, TCurrent, TNext>(handler, nextHandler);
  }

  [Pure]
  public static AsyncHandler<TIn, TNext> Next<TIn, TCurrent, TNext>(
    this AsyncHandler<TIn, TCurrent> handler, Handler<TCurrent, TNext> nextHandler) {
    return new AsyncChainedHandler<TIn, TCurrent, TNext>(handler, nextHandler.ToAsyncHandler());
  }

  [Pure]
  public static AsyncHandler<TIn, TNext> Next<TIn, TCurrent, TNext>(
    this AsyncHandler<TIn, TCurrent> handler, Func<TCurrent, CancellationToken, ValueTask<TNext>> nextHandler) {
    return handler.Next(Handler.FromMethod(nextHandler));
  }

  [Pure]
  public static AsyncHandler<TIn, TNext> Next<TIn, TCurrent, TNext>(
    this AsyncHandler<TIn, TCurrent> handler, Func<TCurrent, TNext> nextHandler) {
    return handler.Next(Handler.FromMethod(nextHandler));
  }

  [Pure]
  public static AsyncHandler<TIn, TOut> Do<TIn, TOut>(this AsyncHandler<TIn, TOut> handler, AsyncSideAction<TOut> action) {
    return handler.Next(new AsyncTransitiveHandler<TOut>(action));
  }

  [Pure]
  public static AsyncHandler<TIn, TOut> Do<TIn, TOut>(this AsyncHandler<TIn, TOut> handler, SideAction<TOut> action) {
    return handler.Next(new TransitiveHandler<TOut>(action));
  }

  [Pure]
  public static AsyncHandler<TIn, TOut> Filter<TIn, TOut>(
    this AsyncHandler<TIn, TOut> handler, Transformer<TIn, TIn> incoming, Transformer<TOut, TOut> outgoing) {
    return handler.Transform(incoming, outgoing);
  }

}