namespace Interactions.Commands;

public class AsyncCancellableCommand<TIn>(AsyncCommand<TIn> undoCommand, int maxStackSize = 256) : AsyncCommand<TIn> {

  public int ClearedElements {
    get => _undoStack.ClearedElements;
    set => _undoStack.ClearedElements = value;
  }

  private readonly TrimmedStack<TIn> _undoStack = new(maxStackSize);

  public override async ValueTask<bool> Execute(TIn input, CancellationToken token = default) {
    if (await base.Execute(input, token)) {
      _undoStack.Push(input);
      return true;
    }

    return false;
  }

  public async ValueTask<bool> Undo(CancellationToken token = default) {
    return (await UndoCore(token)).success;
  }

  protected virtual async ValueTask<Undo<TIn>> UndoCore(CancellationToken token = default) {
    if (!_undoStack.TryPop(out TIn state))
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