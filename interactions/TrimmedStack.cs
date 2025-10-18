namespace Interactions;

public class TrimmedStack<T>(int maxStackSize = 256) {

  public int ClearedElements {
    get => _clearedElements;
    set => _clearedElements = Math.Min(Math.Max(value, 1), maxStackSize);
  }

  private readonly Stack<T> _main = new(maxStackSize);
  private readonly Stack<T> _buffer = new(maxStackSize);

  private int _clearedElements = 128;

  public void Push(T value) {
    _main.Push(value);

    if (_main.Count >= maxStackSize)
      BottomTrimStack();
  }

  public T Pop() {
    return _main.Pop();
  }

  public bool TryPop(out T value) {
    return _main.TryPop(out value);
  }

  public void Clear() {
    _main.Clear();
  }

  private void BottomTrimStack() {
    int targetSize = maxStackSize - _clearedElements;

    if (targetSize == 0) {
      _main.Clear();
      return;
    }

    while (_main.Count > targetSize)
      _buffer.Push(_main.Pop());

    _main.Clear();

    while (_buffer.TryPop(out T value))
      _main.Push(value);
  }

}