namespace Interactions.Handlers;

public delegate TOut Catch<in TException, in TIn, out TOut>(TException exception, TIn input) where TException : Exception;

public delegate ValueTask<TOut> AsyncCatch<in TException, in TIn, TOut>(TException exception, TIn input) where TException : Exception;

internal sealed class CatchHandler<TException, TIn, TOut> : Handler<TIn, TOut> where TException : Exception {

  private readonly Handler<TIn, TOut> _handler;
  private readonly Catch<TException, TIn, TOut> _catch;

  internal CatchHandler(Handler<TIn, TOut> handler, Catch<TException, TIn, TOut> @catch) {
    _handler = handler;
    _catch = @catch;
  }

  protected override TOut HandleCore(TIn input) {
    try {
      return _handler.Handle(input);
    }
    catch (TException e) {
      return _catch(e, input);
    }
  }

}

internal sealed class AsyncCatchHandler<TException, TIn, TOut> : AsyncHandler<TIn, TOut> where TException : Exception {

  private readonly AsyncHandler<TIn, TOut> _handler;
  private readonly AsyncCatch<TException, TIn, TOut> _catch;

  internal AsyncCatchHandler(AsyncHandler<TIn, TOut> handler, AsyncCatch<TException, TIn, TOut> @catch) {
    _handler = handler;
    _catch = @catch;
  }

  protected override async ValueTask<TOut> HandleCore(TIn input, CancellationToken token = default) {
    try {
      return await _handler.Handle(input, token);
    }
    catch (TException e) {
      return await _catch(e, input);
    }
  }

}