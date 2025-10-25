using Interactions.Core;

namespace Interactions;

internal sealed class IdentityHandler<T> : Handler<T, T> {

  internal static IdentityHandler<T> Instance { get; } = new();

  private IdentityHandler() {
  }

  protected internal override T Handle(T input) {
    return input;
  }

}