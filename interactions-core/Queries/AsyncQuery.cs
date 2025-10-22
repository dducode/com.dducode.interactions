using Interactions.Core.Handlers;

namespace Interactions.Core.Queries;

public class AsyncQuery<T1, T2> : AsyncHandleable<T1, T2> {

  private HandlerNode _handlerNode;

  public virtual ValueTask<T2> Send(T1 input, CancellationToken token = default) {
    if (_handlerNode == null)
      throw new MissingHandlerException("Cannot handle query");
    return _handlerNode.HandleRequest(input, token);
  }

  public override IDisposable Handle(AsyncHandler<T1, T2> handler) {
    if (_handlerNode != null)
      throw new InvalidOperationException("Already has handler");
    return _handlerNode = new HandlerNode(this, handler);
  }

  private class HandlerNode(AsyncQuery<T1, T2> parent, AsyncHandler<T1, T2> handler) : IDisposable {

    public ValueTask<T2> HandleRequest(T1 input, CancellationToken token) {
      return handler.Handle(input, token);
    }

    public void Dispose() {
      handler.Dispose();
      parent._handlerNode = null;
    }

  }

}