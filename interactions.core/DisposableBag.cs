namespace Interactions.Core;

public class DisposableBag : List<IDisposable>, IDisposable {

  public void Dispose() {
    using ListPool<Exception>.ListHandle exceptions = ListPool<Exception>.Get();

    for (int i = Count - 1; i >= 0; i--) {
      try {
        this[i].Dispose();
      }
      catch (Exception e) {
        exceptions.Add(e);
      }
    }

    Clear();
    if (exceptions.Count > 0)
      throw new AggregateException(exceptions);
  }

}