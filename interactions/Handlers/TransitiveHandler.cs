using Interactions.Core;

namespace Interactions.Handlers;

internal sealed class TransitiveHandler<T>(Action<T> action) : Handler<T, T> {

  protected internal override T Handle(T input) {
    action(input);
    return input;
  }

}