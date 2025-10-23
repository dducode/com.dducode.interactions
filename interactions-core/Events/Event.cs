namespace Interactions.Core.Events;

public class Event<T> : Handleable<T, Unit> {

  private readonly List<Subscriber> _subscribers = [];

  public virtual void Publish(T obj) {
    foreach (Subscriber subscriber in _subscribers)
      subscriber.Receive(obj);
  }

  public override IDisposable Handle(Handler<T, Unit> handler) {
    var subscriber = new Subscriber(this, handler);
    _subscribers.Add(subscriber);
    return subscriber;
  }

  private void RemoveSubscriber(Subscriber subscriber) {
    _subscribers.Remove(subscriber);
  }

  private class Subscriber(Event<T> parent, Handler<T, Unit> handler) : IDisposable {

    public void Receive(T obj) {
      handler.Handle(obj);
    }

    public void Dispose() {
      handler.Dispose();
      parent.RemoveSubscriber(this);
    }

  }

}