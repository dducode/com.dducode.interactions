namespace Interactions.Queries;

public class AsyncQuery<TIn, TResponse> : AsyncHandleable<TIn, TResponse> {

  private HandlerNode _handlerNode;

  public virtual ValueTask<TResponse> Send(TIn input, CancellationToken token = default) {
    if (_handlerNode == null)
      throw new MissingHandlerException("Cannot handle query");
    return _handlerNode.HandleRequest(input, token);
  }

  public override IDisposable Handle(AsyncHandler<TIn, TResponse> handler) {
    if (_handlerNode != null)
      throw new InvalidOperationException("Already has handler");
    return _handlerNode = new HandlerNode(this, handler);
  }

  private class HandlerNode(AsyncQuery<TIn, TResponse> parent, AsyncHandler<TIn, TResponse> handler) : IDisposable {

    public ValueTask<TResponse> HandleRequest(TIn input, CancellationToken token) {
      return handler.Handle(input, token);
    }

    public void Dispose() {
      handler.Dispose();
      parent._handlerNode = null;
    }

  }

}