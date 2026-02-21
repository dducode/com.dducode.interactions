using Interactions.Core;

namespace Interactions.Pipelines;

internal sealed class AsyncAnonymousPipeline<T1, T2, T3, T4>(AsyncFunc<T1, AsyncHandler<T3, T4>, T2> pipeline) : AsyncPipeline<T1, T2, T3, T4> {

  public override ValueTask<T2> Invoke(T1 input, AsyncHandler<T3, T4> next, CancellationToken token = default) {
    return pipeline(input, next, token);
  }

}

internal sealed class AsyncAnonymousPipeline<T1, T2, T3>(AsyncAction<T1, AsyncHandler<T2, T3>> pipeline) : AsyncPipeline<T1, Unit, T2, T3> {

  public override async ValueTask<Unit> Invoke(T1 input, AsyncHandler<T2, T3> next, CancellationToken token = default) {
    await pipeline(input, next, token);
    return default;
  }

}