namespace Interactions.Commands;

public class ReversibleCommand<TIn>(Command<TIn> undoCommand, int maxStackSize = 256) : CancellableCommand<TIn>(undoCommand, maxStackSize) {

  private readonly Stack<TIn> _redoStack = new();

  public override bool Execute(TIn input) {
    if (base.Execute(input)) {
      _redoStack.Clear();
      return true;
    }

    return false;
  }

  public bool Redo() {
    if (!_redoStack.TryPop(out TIn state))
      return false;

    if (!base.Execute(state)) {
      _redoStack.Push(state);
      return false;
    }

    return true;
  }

  protected override bool UndoCore(out TIn state) {
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