namespace Interactions.Handlers;

internal sealed class ConditionalHandler<T1, T2>(
  Func<bool> condition,
  Handler<T1, T2> mainHandler,
  Handler<T1, T2> otherHandler) : Handler<T1, T2> {

  protected override T2 HandleCore(T1 input) {
    return condition() ? mainHandler.Handle(input) : otherHandler.Handle(input);
  }

}

internal sealed class AsyncConditionalHandler<T1, T2>(
  Func<bool> condition,
  AsyncHandler<T1, T2> mainHandler,
  AsyncHandler<T1, T2> otherHandler) : AsyncHandler<T1, T2> {

  protected override ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    return condition() ? mainHandler.Handle(input, token) : otherHandler.Handle(input, token);
  }

}