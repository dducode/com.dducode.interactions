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

internal sealed class AsyncIdentityHandler<T> : AsyncHandler<T, T> {

  internal static AsyncIdentityHandler<T> Instance { get; } = new();

  private AsyncIdentityHandler() {
  }

  protected internal override ValueTask<T> Handle(T input, CancellationToken token = default) {
    token.ThrowIfCancellationRequested();
    return new ValueTask<T>(input);
  }

}