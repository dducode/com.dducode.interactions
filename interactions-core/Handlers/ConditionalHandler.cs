namespace Interactions.Core.Handlers;

internal sealed class ConditionalHandler<T1, T2>(
  Func<bool> condition,
  Handler<T1, T2> mainHandler,
  Handler<T1, T2> otherHandler) : Handler<T1, T2> {

  protected internal override T2 Handle(T1 input) {
    return condition() ? mainHandler.Handle(input) : otherHandler.Handle(input);
  }

}

internal sealed class AsyncConditionalHandler<T1, T2>(
  Func<bool> condition,
  AsyncHandler<T1, T2> mainHandler,
  AsyncHandler<T1, T2> otherHandler) : AsyncHandler<T1, T2> {

  protected internal override ValueTask<T2> Handle(T1 input, CancellationToken token = default) {
    return condition() ? mainHandler.Handle(input, token) : otherHandler.Handle(input, token);
  }

}