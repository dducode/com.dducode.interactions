namespace Interactions.Commands;

public class AsyncCommand<TIn> : AsyncHandleable<TIn, bool> {

  private HandlerNode _handlerNode;

  public virtual ValueTask<bool> Execute(TIn input, CancellationToken token = default) {
    return _handlerNode?.ExecuteCommand(input, token) ?? new ValueTask<bool>(false);
  }

  public override IDisposable Handle(AsyncHandler<TIn, bool> handler) {
    if (_handlerNode != null)
      throw new InvalidOperationException("Already has handler");
    return _handlerNode = new HandlerNode(this, handler);
  }

  protected virtual void Clear() {
    _handlerNode = null;
  }

  private class HandlerNode(AsyncCommand<TIn> parent, AsyncHandler<TIn, bool> handler) : IDisposable {

    public async ValueTask<bool> ExecuteCommand(TIn input, CancellationToken token) {
      try {
        token.ThrowIfCancellationRequested();
        return await handler.Handle(input, token);
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