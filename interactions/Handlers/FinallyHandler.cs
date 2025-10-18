namespace Interactions.Handlers;

public delegate void Finally<in TIn>(TIn input);

public delegate ValueTask AsyncFinally<in TIn>(TIn input);

internal sealed class FinallyHandler<TIn, TOut> : Handler<TIn, TOut> {

  private readonly Handler<TIn, TOut> _handler;
  private readonly Finally<TIn> _finally;

  internal FinallyHandler(Handler<TIn, TOut> handler, Finally<TIn> @finally) {
    _handler = handler;
    _finally = @finally;
  }

  protected override TOut HandleCore(TIn input) {
    try {
      return _handler.Handle(input);
    }
    finally {
      _finally(input);
    }
  }

}

internal sealed class AsyncFinallyHandler<TIn, TOut> : AsyncHandler<TIn, TOut> {

  private readonly AsyncHandler<TIn, TOut> _handler;
  private readonly AsyncFinally<TIn> _finally;

  internal AsyncFinallyHandler(AsyncHandler<TIn, TOut> handler, AsyncFinally<TIn> @finally) {
    _handler = handler;
    _finally = @finally;
  }

  protected override async ValueTask<TOut> HandleCore(TIn input, CancellationToken token = default) {
    try {
      return await _handler.Handle(input, token);
    }
    finally {
      await _finally(input);
    }
  }

}