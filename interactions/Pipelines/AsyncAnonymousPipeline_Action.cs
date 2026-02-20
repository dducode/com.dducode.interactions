using Interactions.Core;

namespace Interactions.Pipelines;

internal sealed class AsyncAnonymousPipeline_Action<T1, T2, T3>(AsyncAction<T1, AsyncFunc<T2, T3>> pipeline) : AsyncPipeline<T1, Unit, T2, T3> {

  public override async ValueTask<Unit> Invoke(T1 input, AsyncHandler<T2, T3> next, CancellationToken token = default) {
    await pipeline(input, next.Handle, token);
    return default;
  }

}

internal sealed class AsyncAnonymousPipeline_Action<T1, T2>(AsyncAction<T1, AsyncAction<T2>> pipeline) : AsyncPipeline<T1, Unit, T2, Unit> {

  public override async ValueTask<Unit> Invoke(T1 input, AsyncHandler<T2, Unit> next, CancellationToken token = default) {
    await pipeline(input, async (i, t) => await next.Handle(i, t), token);
    return default;
  }

}

internal sealed class AsyncAnonymousPipeline_Action<T>(AsyncAction<T, AsyncAction> pipeline) : AsyncPipeline<T, Unit, Unit, Unit> {

  public override async ValueTask<Unit> Invoke(T input, AsyncHandler<Unit, Unit> next, CancellationToken token = default) {
    await pipeline(input, async t => await next.Handle(default, t), token);
    return default;
  }

}

internal sealed class AsyncAnonymousPipeline_Action(AsyncAction<AsyncAction> pipeline) : AsyncPipeline<Unit, Unit, Unit, Unit> {

  public override async ValueTask<Unit> Invoke(Unit input, AsyncHandler<Unit, Unit> next, CancellationToken token = default) {
    await pipeline(async t => await next.Handle(default, t), token);
    return default;
  }

}