namespace Interactions.Handlers;

internal sealed class ConditionalHandler<TIn, TOut> : Handler<TIn, TOut> {

  private readonly Func<bool> _condition;
  private readonly Handler<TIn, TOut> _mainHandler;
  private readonly Handler<TIn, TOut> _otherHandler;

  internal ConditionalHandler(Func<bool> condition, Handler<TIn, TOut> mainHandler, Handler<TIn, TOut> otherHandler) {
    _condition = condition;
    _mainHandler = mainHandler;
    _otherHandler = otherHandler;
  }

  protected override TOut HandleCore(TIn input) {
    return _condition() ? _mainHandler.Handle(input) : _otherHandler.Handle(input);
  }

}

internal sealed class AsyncConditionalHandler<TIn, TOut> : AsyncHandler<TIn, TOut> {

  private readonly Func<bool> _condition;
  private readonly AsyncHandler<TIn, TOut> _mainHandler;
  private readonly AsyncHandler<TIn, TOut> _otherHandler;

  internal AsyncConditionalHandler(Func<bool> condition, AsyncHandler<TIn, TOut> mainHandler, AsyncHandler<TIn, TOut> otherHandler) {
    _condition = condition;
    _mainHandler = mainHandler;
    _otherHandler = otherHandler;
  }

  protected override ValueTask<TOut> HandleCore(TIn input, CancellationToken token = default) {
    return _condition() ? _mainHandler.Handle(input, token) : _otherHandler.Handle(input, token);
  }

}