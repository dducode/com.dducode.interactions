using Interactions.Handlers;

namespace Interactions.Builders;

public class UseHandlersBuilder<T1, T2> {

  private readonly List<Use<T1, T2>> _delegates = [];

  internal UseHandlersBuilder(Use<T1, T2> func) {
    _delegates.Add(func);
  }

  public UseHandlersBuilder<T1, T2> Use(Use<T1, T2> func) {
    _delegates.Add(func);
    return this;
  }

  public Handler<T1, T2> End(Handler<T1, T2> handler) {
    return _delegates
      .AsEnumerable()
      .Reverse()
      .Aggregate(handler, (current, func) => new UseHandler<T1, T2>(func, current));
  }

}

public class AsyncUseHandlersBuilder<T1, T2> {

  private readonly List<AsyncUse<T1, T2>> _delegates = [];

  internal AsyncUseHandlersBuilder(AsyncUse<T1, T2> func) {
    _delegates.Add(func);
  }

  public AsyncUseHandlersBuilder<T1, T2> Use(AsyncUse<T1, T2> func) {
    _delegates.Add(func);
    return this;
  }

  public AsyncHandler<T1, T2> End(AsyncHandler<T1, T2> handler) {
    return _delegates
      .AsEnumerable()
      .Reverse()
      .Aggregate(handler, (current, func) => new AsyncUseHandler<T1, T2>(func, current));
  }

}