using Interactions.Extensions;

namespace Interactions;

public sealed class History<T>(int capacity = 256) : IDisposable {

  public int Count => _undoStack.Count + _redoStack.Count;

  private readonly Stack<T> _undoStack = new(capacity);
  private readonly Stack<T> _redoStack = new(capacity);
  private readonly Stack<T> _buffer = new(capacity);

  private bool _disposed;

  public void Write(T value) {
    ThrowIfDisposed();
    _redoStack.Clear();
    _undoStack.Push(value);
  }

  public bool Undo(out T value) {
    ThrowIfDisposed();

    if (!_undoStack.TryPop(out value))
      return false;

    _redoStack.Push(value);
    return true;
  }

  public bool Redo(out T value) {
    ThrowIfDisposed();

    if (!_redoStack.TryPop(out value))
      return false;

    _undoStack.Push(value);
    return true;
  }

  public void Clear(int count, ICollection<T> clearedElements = null) {
    if (_disposed)
      return;

    int redoStackCount = _redoStack.Count;
    while (_redoStack.TryPop(out T value))
      _undoStack.Push(value);

    int targetSize = Math.Max(Count - count, 0);

    if (targetSize == 0) {
      clearedElements?.AddRange(_undoStack);
      _undoStack.Clear();
      return;
    }

    while (_undoStack.Count > targetSize)
      _buffer.Push(_undoStack.Pop());

    clearedElements?.AddRange(_undoStack);
    _undoStack.Clear();

    while (_buffer.TryPop(out T value))
      _undoStack.Push(value);

    for (var i = 0; i < redoStackCount; i++)
      _redoStack.Push(_undoStack.Pop());
  }

  public void Clear(ICollection<T> clearedElements = null) {
    if (_disposed)
      return;

    clearedElements?.AddRange(_undoStack);
    _undoStack.Clear();
    clearedElements?.AddRange(_redoStack);
    _redoStack.Clear();
  }

  public void Dispose() {
    if (_disposed)
      return;

    Clear();
    _disposed = true;
  }

  private void ThrowIfDisposed() {
#if NET9_0_OR_GREATER
    ObjectDisposedException.ThrowIf(_disposed, this);
#else
    if (_disposed)
      throw new ObjectDisposedException(nameof(History<T>));
#endif
  }

}