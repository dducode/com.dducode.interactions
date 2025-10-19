namespace Interactions.Commands;

public class CancellableCommand<T>(Command<T> undoCommand, int maxStackSize = 256) : Command<T> {

  public int ClearedElements {
    get => _undoStack.ClearedElements;
    set => _undoStack.ClearedElements = value;
  }

  private readonly TrimmedStack<T> _undoStack = new(maxStackSize);

  public override bool Execute(T input) {
    if (base.Execute(input)) {
      _undoStack.Push(input);
      return true;
    }

    return false;
  }

  public bool Undo() {
    return UndoCore(out _);
  }

  protected virtual bool UndoCore(out T state) {
    if (!_undoStack.TryPop(out state))
      return false;

    if (!undoCommand.Execute(state)) {
      _undoStack.Push(state);
      return false;
    }

    return true;
  }

  protected override void Clear() {
    base.Clear();
    _undoStack.Clear();
  }

}