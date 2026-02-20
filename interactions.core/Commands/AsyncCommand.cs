namespace Interactions.Core.Commands;

public class AsyncCommand<T> : AsyncHandleable<T, Unit> {

  private HandlerNode _handlerNode;

  public virtual ValueTask<bool> Execute(T input, CancellationToken token = default) {
    HandlerNode node = Volatile.Read(ref _handlerNode);
    return node?.ExecuteCommand(input, token) ?? new ValueTask<bool>(false);
  }

  public override IDisposable Handle(AsyncHandler<T, Unit> handler) {
    var node = new HandlerNode(this, handler);
    if (Interlocked.CompareExchange(ref _handlerNode, node, null) != null)
      throw new InvalidOperationException("Already has handler");
    return node;
  }

  protected virtual void Clear() {
    Interlocked.Exchange(ref _handlerNode, null);
  }

  private class HandlerNode(AsyncCommand<T> parent, AsyncHandler<T, Unit> handler) : IDisposable {

    public async ValueTask<bool> ExecuteCommand(T input, CancellationToken token) {
      try {
        await handler.Handle(input, token);
        return true;
      }
      catch (OperationCanceledException) {
        return false;
      }
    }

    public void Dispose() {
      handler.Dispose();
      parent.Clear();
    }

  }

}