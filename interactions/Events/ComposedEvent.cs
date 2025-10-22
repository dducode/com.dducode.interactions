using Interactions.Core;
using Interactions.Core.Events;
using Interactions.Core.Handlers;

namespace Interactions.Events;

internal sealed class ComposedEvent<T, T1, T2>(Event<T1> firstEvent, Event<T2> secondEvent) : Event<T> where T : IDeconstructable<T1, T2> {

  public override void Publish(T obj) {
    (T1 first, T2 second) = obj;
    firstEvent.Publish(first);
    secondEvent.Publish(second);
  }

  public override IDisposable Handle(Handler<T, Unit> handler) {
    throw new InvalidOperationException("Cannot handle composed event");
  }

}

internal sealed class ComposedEvent<T, T1, T2, T3>(
  Event<T1> firstEvent,
  Event<T2> secondEvent,
  Event<T3> thirdEvent) : Event<T> where T : IDeconstructable<T1, T2, T3> {

  public override void Publish(T obj) {
    (T1 first, T2 second, T3 third) = obj;
    firstEvent.Publish(first);
    secondEvent.Publish(second);
    thirdEvent.Publish(third);
  }

  public override IDisposable Handle(Handler<T, Unit> handler) {
    throw new InvalidOperationException("Cannot handle composed event");
  }

}

internal sealed class ComposedEvent<T, T1, T2, T3, T4>(
  Event<T1> firstEvent,
  Event<T2> secondEvent,
  Event<T3> thirdEvent,
  Event<T4> fourthEvent) : Event<T> where T : IDeconstructable<T1, T2, T3, T4> {

  public override void Publish(T obj) {
    (T1 first, T2 second, T3 third, T4 fourth) = obj;
    firstEvent.Publish(first);
    secondEvent.Publish(second);
    thirdEvent.Publish(third);
    fourthEvent.Publish(fourth);
  }

  public override IDisposable Handle(Handler<T, Unit> handler) {
    throw new InvalidOperationException("Cannot handle composed event");
  }

}