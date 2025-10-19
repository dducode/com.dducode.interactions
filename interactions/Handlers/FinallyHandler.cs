namespace Interactions.Handlers;

public delegate void Finally<in T>(T input);

public delegate ValueTask AsyncFinally<in T>(T input);

internal sealed class FinallyHandler<T1, T2> : Handler<T1, T2> {

  private readonly Handler<T1, T2> _handler;
  private readonly Finally<T1> _finally;

  internal FinallyHandler(Handler<T1, T2> handler, Finally<T1> @finally) {
    _handler = handler;
    _finally = @finally;
  }

  protected override T2 HandleCore(T1 input) {
    try {
      return _handler.Handle(input);
    }
    finally {
      _finally(input);
    }
  }

}

internal sealed class AsyncFinallyHandler<T1, T2> : AsyncHandler<T1, T2> {

  private readonly AsyncHandler<T1, T2> _handler;
  private readonly AsyncFinally<T1> _finally;

  internal AsyncFinallyHandler(AsyncHandler<T1, T2> handler, AsyncFinally<T1> @finally) {
    _handler = handler;
    _finally = @finally;
  }

  protected override async ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    try {
      return await _handler.Handle(input, token);
    }
    finally {
      await _finally(input);
    }
  }

}