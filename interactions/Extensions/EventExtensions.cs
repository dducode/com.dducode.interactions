using System.Diagnostics.Contracts;
using Interactions.Core.Events;
using Interactions.Events;

namespace Interactions.Extensions;

public static class EventExtensions {

  [Pure]
  public static Event<T> Group<T>(this Event<T> @event, params Event<T>[] events) {
    return new GroupedEvent<T>(Enumerable.Empty<Event<T>>().Append(@event).Concat(events));
  }

  [Pure]
  public static Event<T> Compose<T, T1, T2>(this Event<T1> first, Event<T2> second) where T : IDeconstructable<T1, T2> {
    return new ComposedEvent<T, T1, T2>(first, second);
  }

  [Pure]
  public static Event<T> Compose<T, T1, T2, T3>(this Event<T1> first, Event<T2> second, Event<T3> third) where T : IDeconstructable<T1, T2, T3> {
    return new ComposedEvent<T, T1, T2, T3>(first, second, third);
  }

  [Pure]
  public static Event<T> Compose<T, T1, T2, T3, T4>(
    this Event<T1> first, Event<T2> second, Event<T3> third, Event<T4> fourth) where T : IDeconstructable<T1, T2, T3, T4> {
    return new ComposedEvent<T, T1, T2, T3, T4>(first, second, third, fourth);
  }

}