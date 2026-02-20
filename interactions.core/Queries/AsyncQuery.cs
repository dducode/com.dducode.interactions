namespace Interactions.Core.Queries;

public class AsyncQuery<T1, T2> : AsyncHandleable<T1, T2> {

  private HandlerNode _handlerNode;

  public virtual ValueTask<T2> Send(T1 input, CancellationToken token = default) {
    HandlerNode node = Volatile.Read(ref _handlerNode);
    if (node == null)
      throw new MissingHandlerException("Cannot handle query");
    return node.HandleRequest(input, token);
  }

  public override IDisposable Handle(AsyncHandler<T1, T2> handler) {
    var node = new HandlerNode(this, handler);
    if (Interlocked.CompareExchange(ref _handlerNode, node, null) != null)
      throw new InvalidOperationException("Already has handler");
    return node;
  }

  private void Clear() {
    Interlocked.Exchange(ref _handlerNode, null);
  }

  private class HandlerNode(AsyncQuery<T1, T2> parent, AsyncHandler<T1, T2> handler) : IDisposable {

    public ValueTask<T2> HandleRequest(T1 input, CancellationToken token) {
      return handler.Handle(input, token);
    }

    public void Dispose() {
      handler.Dispose();
      parent.Clear();
    }

  }

}