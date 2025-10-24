using Interactions.Core;

namespace Interactions.Pipelines;

internal sealed class AsyncAnonymousPipeline_Func<T1, T2, T3, T4>(AsyncFunc<T1, AsyncFunc<T3, T4>, T2> pipeline) : AsyncPipeline<T1, T2, T3, T4> {

  protected internal override ValueTask<T2> Invoke(T1 input, AsyncFunc<T3, T4> next, CancellationToken token = default) {
    return pipeline(input, next, token);
  }

}

internal sealed class AsyncAnonymousPipeline_Func<T1, T2, T3>(AsyncFunc<T1, AsyncAction<T3>, T2> pipeline) : AsyncPipeline<T1, T2, T3, Unit> {

  protected internal override ValueTask<T2> Invoke(T1 input, AsyncFunc<T3, Unit> next, CancellationToken token = default) {
    return pipeline(input, async (i, t) => await next(i, t), token);
  }

}

internal sealed class AsyncAnonymousPipeline_Func<T1, T2>(AsyncFunc<T1, AsyncAction, T2> pipeline) : AsyncPipeline<T1, T2, Unit, Unit> {

  protected internal override ValueTask<T2> Invoke(T1 input, AsyncFunc<Unit, Unit> next, CancellationToken token = default) {
    return pipeline(input, async t => await next(default, t), token);
  }

}