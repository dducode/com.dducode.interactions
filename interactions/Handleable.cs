namespace Interactions;

public abstract class Handleable<TIn, TOut> {

  public abstract IDisposable Handle(Handler<TIn, TOut> handler);

}

public abstract class AsyncHandleable<TIn, TOut> {

  public abstract IDisposable Handle(AsyncHandler<TIn, TOut> handler);

}