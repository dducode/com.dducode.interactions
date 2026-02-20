namespace Interactions.Core.Handlers;

internal sealed class IdentityHandler<T> : Handler<T, T> {

  internal static IdentityHandler<T> Instance { get; } = new();

  private IdentityHandler() {
  }

  public override T Handle(T input) {
    return input;
  }

}