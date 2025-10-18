namespace Interactions.Handlers;

public delegate void SideAction<in TIn>(TIn input);

public delegate ValueTask AsyncSideAction<in TIn>(TIn input, CancellationToken token = default);

internal sealed class TransitiveHandler<TIn> : Handler<TIn, TIn> {

  private readonly SideAction<TIn> _action;

  internal TransitiveHandler(SideAction<TIn> action) {
    _action = action;
  }

  protected override TIn HandleCore(TIn input) {
    _action(input);
    return input;
  }

}

internal sealed class AsyncTransitiveHandler<TIn> : AsyncHandler<TIn, TIn> {

  private readonly AsyncSideAction<TIn> _action;

  internal AsyncTransitiveHandler(AsyncSideAction<TIn> action) {
    _action = action;
  }

  protected override async ValueTask<TIn> HandleCore(TIn input, CancellationToken token = default) {
    await _action(input, token);
    return input;
  }

}