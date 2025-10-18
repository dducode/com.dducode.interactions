namespace Interactions.Commands;

public class Command<T> : Handleable<T, bool> {

  private HandlerNode _handlerNode;

  public virtual bool Execute(T input) {
    return _handlerNode?.ExecuteCommand(input) ?? false;
  }

  public override IDisposable Handle(Handler<T, bool> handler) {
    if (_handlerNode != null)
      throw new InvalidOperationException("Already has handler");
    return _handlerNode = new HandlerNode(this, handler);
  }

  protected virtual void Clear() {
    _handlerNode = null;
  }

  private class HandlerNode(Command<T> parent, Handler<T, bool> handler) : IDisposable {

    public bool ExecuteCommand(T input) {
      return handler.Handle(input);
    }

    public void Dispose() {
      handler.Dispose();
      parent.Clear();
    }

  }

}