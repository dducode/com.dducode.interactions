namespace Interactions.Core.Commands;

public interface ICommand<in T> {

  bool Execute(T input);

}

public class Command<T> : Handleable<T, Unit>, ICommand<T> {

  private HandlerNode _handlerNode;

  public virtual bool Execute(T input) {
    return Volatile.Read(ref _handlerNode)?.ExecuteCommand(input) ?? false;
  }

  public override IDisposable Handle(Handler<T, Unit> handler) {
    var node = new HandlerNode(this, handler);
    if (Interlocked.CompareExchange(ref _handlerNode, node, null) != null)
      throw new InvalidOperationException("Already has handler");
    return node;
  }

  private void Clear() {
    Interlocked.Exchange(ref _handlerNode, null);
  }

  private class HandlerNode(Command<T> parent, Handler<T, Unit> handler) : IDisposable {

    public bool ExecuteCommand(T input) {
      handler.Handle(input);
      return true;
    }

    public void Dispose() {
      handler.Dispose();
      parent.Clear();
    }

  }

}