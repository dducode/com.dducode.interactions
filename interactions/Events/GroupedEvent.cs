namespace Interactions.Events;

internal sealed class GroupedEvent<T>(IEnumerable<Event<T>> events) : Event<T> {

  public override void Publish(T obj) {
    foreach (Event<T> @event in events)
      @event.Publish(obj);
  }

  public override IDisposable Handle(Handler<T, Unit> handler) {
    throw new InvalidOperationException("Cannot handle grouped event");
  }

}