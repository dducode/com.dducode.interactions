namespace Interactions.Queries;

public class Query<T1, T2> : Handleable<T1, T2> {

  private HandlerNode _handlerNode;

  public virtual T2 Send(T1 input) {
    if (_handlerNode == null)
      throw new MissingHandlerException("Cannot handle query");
    return _handlerNode.HandleRequest(input);
  }

  public override IDisposable Handle(Handler<T1, T2> handler) {
    if (_handlerNode != null)
      throw new InvalidOperationException("Already has handler");
    return _handlerNode = new HandlerNode(this, handler);
  }

  protected virtual void Clear() {
    _handlerNode = null;
  }

  private class HandlerNode(Query<T1, T2> parent, Handler<T1, T2> handler) : IDisposable {

    public T2 HandleRequest(T1 request) {
      return handler.Handle(request);
    }

    public void Dispose() {
      handler.Dispose();
      parent.Clear();
    }

  }

}