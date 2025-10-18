using Interactions.Handlers;

namespace Interactions.Builders;

public class ConditionalHandlersBuilder<TIn, TOut> {

  private readonly List<(Func<bool> condition, Handler<TIn, TOut> handler)> _nodes = [];

  private ConditionalHandlersBuilder(Func<bool> condition, Handler<TIn, TOut> handler) {
    _nodes.Add((condition, handler));
  }

  internal static ConditionalHandlersBuilder<TIn, TOut> If(Func<bool> condition, Handler<TIn, TOut> handler) {
    return new ConditionalHandlersBuilder<TIn, TOut>(condition, handler);
  }

  public ConditionalHandlersBuilder<TIn, TOut> ElseIf(Func<bool> condition, Handler<TIn, TOut> handler) {
    _nodes.Add((condition, handler));
    return this;
  }

  public Handler<TIn, TOut> Else(Handler<TIn, TOut> handler) {
    return _nodes
      .AsEnumerable()
      .Reverse()
      .Aggregate(handler, (current, node) => new ConditionalHandler<TIn, TOut>(node.condition, node.handler, current));
  }

}

public class AsyncConditionalHandlersBuilder<TIn, TOut> {

  private readonly List<(Func<bool> condition, AsyncHandler<TIn, TOut> handler)> _nodes = [];

  private AsyncConditionalHandlersBuilder(Func<bool> condition, AsyncHandler<TIn, TOut> handler) {
    _nodes.Add((condition, handler));
  }

  internal static AsyncConditionalHandlersBuilder<TIn, TOut> If(Func<bool> condition, AsyncHandler<TIn, TOut> handler) {
    return new AsyncConditionalHandlersBuilder<TIn, TOut>(condition, handler);
  }

  public AsyncConditionalHandlersBuilder<TIn, TOut> ElseIf(Func<bool> condition, AsyncHandler<TIn, TOut> handler) {
    _nodes.Add((condition, handler));
    return this;
  }

  public AsyncHandler<TIn, TOut> Else(AsyncHandler<TIn, TOut> handler) {
    return _nodes
      .AsEnumerable()
      .Reverse()
      .Aggregate(handler, (current, node) => new AsyncConditionalHandler<TIn, TOut>(node.condition, node.handler, current));
  }

}