using Interactions.Core;

namespace Interactions.Pipelines;

internal sealed class AsyncAnonymousPipeline_Func<T1, T2, T3, T4>(AsyncFunc<T1, AsyncFunc<T3, T4>, T2> pipeline) : AsyncPipeline<T1, T2, T3, T4> {

  public override ValueTask<T2> Invoke(T1 input, AsyncHandler<T3, T4> next, CancellationToken token = default) {
    return pipeline(input, next.Handle, token);
  }

}

internal sealed class AsyncAnonymousPipeline_Func<T1, T2, T3>(AsyncFunc<T1, AsyncAction<T3>, T2> pipeline) : AsyncPipeline<T1, T2, T3, Unit> {

  public override ValueTask<T2> Invoke(T1 input, AsyncHandler<T3, Unit> next, CancellationToken token = default) {
    return pipeline(input, async (i, t) => await next.Handle(i, t), token);
  }

}

internal sealed class AsyncAnonymousPipeline_Func<T1, T2>(AsyncFunc<T1, AsyncAction, T2> pipeline) : AsyncPipeline<T1, T2, Unit, Unit> {

  public override ValueTask<T2> Invoke(T1 input, AsyncHandler<Unit, Unit> next, CancellationToken token = default) {
    return pipeline(input, async t => await next.Handle(default, t), token);
  }

}