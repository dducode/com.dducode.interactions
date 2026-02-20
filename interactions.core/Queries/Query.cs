namespace Interactions.Core.Queries;

public class Query<T1, T2> : Handleable<T1, T2> {

  private HandlerNode _handlerNode;

  public virtual T2 Send(T1 input) {
    HandlerNode node = Volatile.Read(ref _handlerNode);
    if (node == null)
      throw new MissingHandlerException("Cannot handle query");
    return node.HandleRequest(input);
  }

  public override IDisposable Handle(Handler<T1, T2> handler) {
    var node = new HandlerNode(this, handler);
    if (Interlocked.CompareExchange(ref _handlerNode, node, null) != null)
      throw new InvalidOperationException("Already has handler");
    return node;
  }

  private void Clear() {
    Interlocked.Exchange(ref _handlerNode, null);
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