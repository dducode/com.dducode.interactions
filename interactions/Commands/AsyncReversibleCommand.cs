using Interactions.Extensions;

namespace Interactions.Commands;

public class AsyncReversibleCommand<T>(AsyncCommand<T> undoCommand, int maxStackSize = 256) : AsyncCancellableCommand<T>(undoCommand, maxStackSize) {

  private readonly Stack<T> _redoStack = new();

  public override async ValueTask<bool> Execute(T input, CancellationToken token = default) {
    if (await base.Execute(input, token)) {
      _redoStack.Clear();
      return true;
    }

    return false;
  }

  public async ValueTask<bool> Redo(CancellationToken token = default) {
    if (!_redoStack.TryPop(out T prevState))
      return false;

    if (!await base.Execute(prevState, token)) {
      _redoStack.Push(prevState);
      return false;
    }

    return true;
  }

  protected override async ValueTask<Undo<T>> UndoCore(CancellationToken token = default) {
    Undo<T> result = await base.UndoCore(token);
    if (result.success)
      _redoStack.Push(result.value);

    return result;
  }

  protected override void Clear() {
    base.Clear();
    _redoStack.Clear();
  }

}