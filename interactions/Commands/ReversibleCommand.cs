using Interactions.Extensions;

namespace Interactions.Commands;

public class ReversibleCommand<T>(Command<T> undoCommand, int maxStackSize = 256) : CancellableCommand<T>(undoCommand, maxStackSize) {

  private readonly Stack<T> _redoStack = new();

  public override bool Execute(T input) {
    if (base.Execute(input)) {
      _redoStack.Clear();
      return true;
    }

    return false;
  }

  public bool Redo() {
    if (!_redoStack.TryPop(out T state))
      return false;

    if (!base.Execute(state)) {
      _redoStack.Push(state);
      return false;
    }

    return true;
  }

  protected override bool UndoCore(out T state) {
    if (base.UndoCore(out state)) {
      _redoStack.Push(state);
      return true;
    }

    return false;
  }

  protected override void Clear() {
    base.Clear();
    _redoStack.Clear();
  }

}