using System.Collections;

namespace Interactions.Core;

internal static class ListPool<T> {

  private static readonly Stack<List<T>> _pool = new();

  internal static ListHandle Get() {
    List<T> list;
    lock (_pool)
      list = _pool.Count > 0 ? _pool.Pop() : [];
    return new ListHandle(list);
  }

  private static void Return(List<T> list) {
    lock (_pool)
      _pool.Push(list);
  }

  internal readonly struct ListHandle(List<T> list) : IDisposable, IEnumerable<T> {

    public int Count => list.Count;

    public void Add(T item) {
      list.Add(item);
    }

    public void AddRange(IEnumerable<T> items) {
      list.AddRange(items);
    }

    public void Dispose() {
      list.Clear();
      Return(list);
    }

    public List<T>.Enumerator GetEnumerator() {
      return list.GetEnumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator() {
      return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }

  }

}