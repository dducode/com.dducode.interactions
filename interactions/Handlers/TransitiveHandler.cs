namespace Interactions.Handlers;

public delegate void SideAction<in T>(T input);

public delegate ValueTask AsyncSideAction<in T>(T input, CancellationToken token = default);

internal sealed class TransitiveHandler<T>(SideAction<T> action) : Handler<T, T> {

  protected override T HandleCore(T input) {
    action(input);
    return input;
  }

}

internal sealed class AsyncTransitiveHandler<T>(AsyncSideAction<T> action) : AsyncHandler<T, T> {

  protected override async ValueTask<T> HandleCore(T input, CancellationToken token = default) {
    await action(input, token);
    return input;
  }

}