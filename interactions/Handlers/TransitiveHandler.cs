using Interactions.Core;

namespace Interactions.Handlers;

internal sealed class TransitiveHandler<T>(Action<T> action) : Handler<T, T> {

  public override T Handle(T input) {
    action(input);
    return input;
  }

}