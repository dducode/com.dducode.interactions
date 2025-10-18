namespace Interactions.Events;

internal sealed class GroupedEvent<T> : Event<T> {

  private readonly IEnumerable<Event<T>> _events;

  internal GroupedEvent(IEnumerable<Event<T>> events) {
    _events = events;
  }

  public override void Publish(T obj) {
    foreach (Event<T> @event in _events)
      @event.Publish(obj);
  }

  public override IDisposable Handle(Handler<T, Unit> handler) {
    throw new InvalidOperationException("Cannot handle grouped event");
  }

}