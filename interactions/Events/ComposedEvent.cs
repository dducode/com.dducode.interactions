namespace Interactions.Events;

internal sealed class ComposedEvent<T, T1, T2> : Event<T> where T : IDeconstructable<T1, T2> {

  private readonly Event<T1> _firstEvent;
  private readonly Event<T2> _secondEvent;

  internal ComposedEvent(Event<T1> firstEvent, Event<T2> secondEvent) {
    _firstEvent = firstEvent;
    _secondEvent = secondEvent;
  }

  public override void Publish(T obj) {
    (T1 first, T2 second) = obj;
    _firstEvent.Publish(first);
    _secondEvent.Publish(second);
  }

  public override IDisposable Handle(Handler<T, Unit> handler) {
    throw new InvalidOperationException("Cannot handle composed event");
  }

}

internal sealed class ComposedEvent<T, T1, T2, T3> : Event<T> where T : IDeconstructable<T1, T2, T3> {

  private readonly Event<T1> _firstEvent;
  private readonly Event<T2> _secondEvent;
  private readonly Event<T3> _thirdEvent;

  internal ComposedEvent(Event<T1> firstEvent, Event<T2> secondEvent, Event<T3> thirdEvent) {
    _firstEvent = firstEvent;
    _secondEvent = secondEvent;
    _thirdEvent = thirdEvent;
  }

  public override void Publish(T obj) {
    (T1 first, T2 second, T3 third) = obj;
    _firstEvent.Publish(first);
    _secondEvent.Publish(second);
    _thirdEvent.Publish(third);
  }

  public override IDisposable Handle(Handler<T, Unit> handler) {
    throw new InvalidOperationException("Cannot handle composed event");
  }

}

internal sealed class ComposedEvent<T, T1, T2, T3, T4> : Event<T> where T : IDeconstructable<T1, T2, T3, T4> {

  private readonly Event<T1> _firstEvent;
  private readonly Event<T2> _secondEvent;
  private readonly Event<T3> _thirdEvent;
  private readonly Event<T4> _fourthEvent;

  internal ComposedEvent(Event<T1> firstEvent, Event<T2> secondEvent, Event<T3> thirdEvent, Event<T4> fourthEvent) {
    _firstEvent = firstEvent;
    _secondEvent = secondEvent;
    _thirdEvent = thirdEvent;
    _fourthEvent = fourthEvent;
  }

  public override void Publish(T obj) {
    (T1 first, T2 second, T3 third, T4 fourth) = obj;
    _firstEvent.Publish(first);
    _secondEvent.Publish(second);
    _thirdEvent.Publish(third);
    _fourthEvent.Publish(fourth);
  }

  public override IDisposable Handle(Handler<T, Unit> handler) {
    throw new InvalidOperationException("Cannot handle composed event");
  }

}