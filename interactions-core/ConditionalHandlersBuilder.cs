using Interactions.Core.Handlers;

namespace Interactions.Core;

public class ConditionalHandlersBuilder<T1, T2> {

  private readonly List<(Func<bool> condition, Handler<T1, T2> handler)> _nodes = [];

  private ConditionalHandlersBuilder(Func<bool> condition, Handler<T1, T2> handler) {
    _nodes.Add((condition, handler));
  }

  internal static ConditionalHandlersBuilder<T1, T2> If(Func<bool> condition, Handler<T1, T2> handler) {
    return new ConditionalHandlersBuilder<T1, T2>(condition, handler);
  }

  public ConditionalHandlersBuilder<T1, T2> ElseIf(Func<bool> condition, Handler<T1, T2> handler) {
    _nodes.Add((condition, handler));
    return this;
  }

  public Handler<T1, T2> Else(Handler<T1, T2> handler) {
    return _nodes
      .AsEnumerable()
      .Reverse()
      .Aggregate(handler, (current, node) => new ConditionalHandler<T1, T2>(node.condition, node.handler, current));
  }

}

public class AsyncConditionalHandlersBuilder<T1, T2> {

  private readonly List<(Func<bool> condition, AsyncHandler<T1, T2> handler)> _nodes = [];

  private AsyncConditionalHandlersBuilder(Func<bool> condition, AsyncHandler<T1, T2> handler) {
    _nodes.Add((condition, handler));
  }

  internal static AsyncConditionalHandlersBuilder<T1, T2> If(Func<bool> condition, AsyncHandler<T1, T2> handler) {
    return new AsyncConditionalHandlersBuilder<T1, T2>(condition, handler);
  }

  public AsyncConditionalHandlersBuilder<T1, T2> ElseIf(Func<bool> condition, AsyncHandler<T1, T2> handler) {
    _nodes.Add((condition, handler));
    return this;
  }

  public AsyncHandler<T1, T2> Else(AsyncHandler<T1, T2> handler) {
    return _nodes
      .AsEnumerable()
      .Reverse()
      .Aggregate(handler, (current, node) => new AsyncConditionalHandler<T1, T2>(node.condition, node.handler, current));
  }

}