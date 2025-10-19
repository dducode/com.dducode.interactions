namespace Interactions.Handlers;

public delegate void Finally<in T>(T input);

public delegate ValueTask AsyncFinally<in T>(T input);

internal sealed class FinallyHandler<T1, T2>(Handler<T1, T2> handler, Finally<T1> @finally) : Handler<T1, T2> {

  protected override T2 HandleCore(T1 input) {
    try {
      return handler.Handle(input);
    }
    finally {
      @finally(input);
    }
  }

}

internal sealed class AsyncFinallyHandler<T1, T2>(AsyncHandler<T1, T2> handler, AsyncFinally<T1> @finally) : AsyncHandler<T1, T2> {

  protected override async ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    try {
      return await handler.Handle(input, token);
    }
    finally {
      await @finally(input);
    }
  }

}