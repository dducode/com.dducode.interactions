namespace Interactions.Core.Events;

public sealed class Event<T> : Handleable<T, Unit> {

  private readonly List<Subscriber> _subscribers = [];

  public void Publish(T input) {
    using ListPool<Subscriber>.ListHandle subscribers = ListPool<Subscriber>.Get();
    lock (_subscribers)
      subscribers.AddRange(_subscribers);

    using ListPool<Exception>.ListHandle exceptions = ListPool<Exception>.Get();

    foreach (Subscriber subscriber in subscribers) {
      try {
        subscriber.Receive(input);
      }
      catch (Exception e) {
        exceptions.Add(e);
      }
    }

    if (exceptions.Count > 0)
      throw new AggregateException(exceptions);
  }

  public override IDisposable Handle(Handler<T, Unit> handler) {
    var subscriber = new Subscriber(this, handler);
    lock (_subscribers)
      _subscribers.Add(subscriber);
    return subscriber;
  }

  private void RemoveSubscriber(Subscriber subscriber) {
    lock (_subscribers)
      _subscribers.Remove(subscriber);
  }

  private class Subscriber(Event<T> parent, Handler<T, Unit> handler) : IDisposable {

    public void Receive(T input) {
      handler.Handle(input);
    }

    public void Dispose() {
      parent.RemoveSubscriber(this);
      handler.Dispose();
    }

  }

}