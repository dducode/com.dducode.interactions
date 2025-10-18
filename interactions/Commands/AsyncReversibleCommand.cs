using Interactions.Extensions;

namespace Interactions.Commands;

public class AsyncReversibleCommand<TIn>(AsyncCommand<TIn> undoCommand, int maxStackSize = 256)
  : AsyncCancellableCommand<TIn>(undoCommand, maxStackSize) {

  private readonly Stack<TIn> _redoStack = new();

  public override async ValueTask<bool> Execute(TIn input, CancellationToken token = default) {
    if (await base.Execute(input, token)) {
      _redoStack.Clear();
      return true;
    }

    return false;
  }

  public async ValueTask<bool> Redo(CancellationToken token = default) {
    if (!_redoStack.TryPop(out TIn prevState))
      return false;

    if (!await base.Execute(prevState, token)) {
      _redoStack.Push(prevState);
      return false;
    }

    return true;
  }

  protected override async ValueTask<Undo<TIn>> UndoCore(CancellationToken token = default) {
    Undo<TIn> result = await base.UndoCore(token);
    if (result.success)
      _redoStack.Push(result.value);

    return result;
  }

  protected override void Clear() {
    base.Clear();
    _redoStack.Clear();
  }

}