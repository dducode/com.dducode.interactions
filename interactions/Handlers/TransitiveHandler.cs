namespace Interactions.Handlers;

public delegate void SideAction<in T>(T input);

public delegate ValueTask AsyncSideAction<in T>(T input, CancellationToken token = default);

internal sealed class TransitiveHandler<T> : Handler<T, T> {

  private readonly SideAction<T> _action;

  internal TransitiveHandler(SideAction<T> action) {
    _action = action;
  }

  protected override T HandleCore(T input) {
    _action(input);
    return input;
  }

}

internal sealed class AsyncTransitiveHandler<T> : AsyncHandler<T, T> {

  private readonly AsyncSideAction<T> _action;

  internal AsyncTransitiveHandler(AsyncSideAction<T> action) {
    _action = action;
  }

  protected override async ValueTask<T> HandleCore(T input, CancellationToken token = default) {
    await _action(input, token);
    return input;
  }

}