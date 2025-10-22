using Interactions.Core.Handlers;

namespace Interactions.Core;

public abstract class Handleable<T1, T2> {

  public abstract IDisposable Handle(Handler<T1, T2> handler);

}

public abstract class AsyncHandleable<T1, T2> {

  public abstract IDisposable Handle(AsyncHandler<T1, T2> handler);

}