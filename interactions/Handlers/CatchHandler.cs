namespace Interactions.Handlers;

public delegate T2 Catch<in TException, in T1, out T2>(TException exception, T1 input) where TException : Exception;

public delegate ValueTask<T2> AsyncCatch<in TException, in T1, T2>(TException exception, T1 input) where TException : Exception;

internal sealed class CatchHandler<TException, T1, T2> : Handler<T1, T2> where TException : Exception {

  private readonly Handler<T1, T2> _handler;
  private readonly Catch<TException, T1, T2> _catch;

  internal CatchHandler(Handler<T1, T2> handler, Catch<TException, T1, T2> @catch) {
    _handler = handler;
    _catch = @catch;
  }

  protected override T2 HandleCore(T1 input) {
    try {
      return _handler.Handle(input);
    }
    catch (TException e) {
      return _catch(e, input);
    }
  }

}

internal sealed class AsyncCatchHandler<TException, T1, T2> : AsyncHandler<T1, T2> where TException : Exception {

  private readonly AsyncHandler<T1, T2> _handler;
  private readonly AsyncCatch<TException, T1, T2> _catch;

  internal AsyncCatchHandler(AsyncHandler<T1, T2> handler, AsyncCatch<TException, T1, T2> @catch) {
    _handler = handler;
    _catch = @catch;
  }

  protected override async ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    try {
      return await _handler.Handle(input, token);
    }
    catch (TException e) {
      return await _catch(e, input);
    }
  }

}