using Interactions.Actions;
using Interactions.Core.Handlers;

namespace Interactions.Handlers;

internal sealed class TransitiveHandler<T>(SideAction<T> action) : Handler<T, T> {

  protected internal override T Handle(T input) {
    action(input);
    return input;
  }

}