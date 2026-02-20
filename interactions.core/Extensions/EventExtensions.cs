using Interactions.Core.Events;

namespace Interactions.Core.Extensions;

public static class EventExtensions {

  public static void Publish(this Event<Unit> @event) {
    @event.Publish(default);
  }

}