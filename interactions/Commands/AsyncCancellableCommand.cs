namespace Interactions.Commands;

public class AsyncCancellableCommand<T>(AsyncCommand<T> undoCommand, int maxStackSize = 256) : AsyncCommand<T> {

  public int ClearedElements {
    get => _undoStack.ClearedElements;
    set => _undoStack.ClearedElements = value;
  }

  private readonly TrimmedStack<T> _undoStack = new(maxStackSize);

  public override async ValueTask<bool> Execute(T input, CancellationToken token = default) {
    if (await base.Execute(input, token)) {
      _undoStack.Push(input);
      return true;
    }

    return false;
  }

  public async ValueTask<bool> Undo(CancellationToken token = default) {
    return (await UndoCore(token)).success;
  }

  protected virtual async ValueTask<Undo<T>> UndoCore(CancellationToken token = default) {
    if (!_undoStack.TryPop(out T state))
      return default;

    if (!await undoCommand.Execute(state, token)) {
      _undoStack.Push(state);
      return default;
    }

    return Interactions.Undo.FromResult(state);
  }

  protected override void Clear() {
    base.Clear();
    _undoStack.Clear();
  }

}