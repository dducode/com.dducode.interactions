namespace Interactions.Handlers;

internal sealed class ConditionalHandler<T1, T2> : Handler<T1, T2> {

  private readonly Func<bool> _condition;
  private readonly Handler<T1, T2> _mainHandler;
  private readonly Handler<T1, T2> _otherHandler;

  internal ConditionalHandler(Func<bool> condition, Handler<T1, T2> mainHandler, Handler<T1, T2> otherHandler) {
    _condition = condition;
    _mainHandler = mainHandler;
    _otherHandler = otherHandler;
  }

  protected override T2 HandleCore(T1 input) {
    return _condition() ? _mainHandler.Handle(input) : _otherHandler.Handle(input);
  }

}

internal sealed class AsyncConditionalHandler<T1, T2> : AsyncHandler<T1, T2> {

  private readonly Func<bool> _condition;
  private readonly AsyncHandler<T1, T2> _mainHandler;
  private readonly AsyncHandler<T1, T2> _otherHandler;

  internal AsyncConditionalHandler(Func<bool> condition, AsyncHandler<T1, T2> mainHandler, AsyncHandler<T1, T2> otherHandler) {
    _condition = condition;
    _mainHandler = mainHandler;
    _otherHandler = otherHandler;
  }

  protected override ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    return _condition() ? _mainHandler.Handle(input, token) : _otherHandler.Handle(input, token);
  }

}