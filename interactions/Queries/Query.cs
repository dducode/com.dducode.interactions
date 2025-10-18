namespace Interactions.Queries;

public class Query<TIn, TResponse> : Handleable<TIn, TResponse> {

  private HandlerNode _handlerNode;

  public virtual TResponse Send(TIn input) {
    if (_handlerNode == null)
      throw new MissingHandlerException("Cannot handle query");
    return _handlerNode.HandleRequest(input);
  }

  public override IDisposable Handle(Handler<TIn, TResponse> handler) {
    if (_handlerNode != null)
      throw new InvalidOperationException("Already has handler");
    return _handlerNode = new HandlerNode(this, handler);
  }

  private class HandlerNode(Query<TIn, TResponse> parent, Handler<TIn, TResponse> handler) : IDisposable {

    public TResponse HandleRequest(TIn request) {
      return handler.Handle(request);
    }

    public void Dispose() {
      handler.Dispose();
      parent._handlerNode = null;
    }

  }

}