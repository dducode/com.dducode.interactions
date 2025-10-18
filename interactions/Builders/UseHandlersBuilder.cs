using Interactions.Handlers;

namespace Interactions.Builders;

public class UseHandlersBuilder<TIn, TOut> {

  private readonly List<Use<TIn, TOut>> _delegates = [];

  internal UseHandlersBuilder(Use<TIn, TOut> func) {
    _delegates.Add(func);
  }

  public UseHandlersBuilder<TIn, TOut> Use(Use<TIn, TOut> func) {
    _delegates.Add(func);
    return this;
  }

  public Handler<TIn, TOut> End(Handler<TIn, TOut> handler) {
    return _delegates
      .AsEnumerable()
      .Reverse()
      .Aggregate(handler, (current, func) => new UseHandler<TIn, TOut>(func, current));
  }

}

public class AsyncUseHandlersBuilder<TIn, TOut> {

  private readonly List<AsyncUse<TIn, TOut>> _delegates = [];

  internal AsyncUseHandlersBuilder(AsyncUse<TIn, TOut> func) {
    _delegates.Add(func);
  }

  public AsyncUseHandlersBuilder<TIn, TOut> Use(AsyncUse<TIn, TOut> func) {
    _delegates.Add(func);
    return this;
  }

  public AsyncHandler<TIn, TOut> End(AsyncHandler<TIn, TOut> handler) {
    return _delegates
      .AsEnumerable()
      .Reverse()
      .Aggregate(handler, (current, func) => new AsyncUseHandler<TIn, TOut>(func, current));
  }

}