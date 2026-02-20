using Interactions.Core;

namespace Interactions.Pipelines;

public abstract class AsyncPipeline<T1, T2, T3, T4> {

  public abstract ValueTask<T2> Invoke(T1 input, AsyncHandler<T3, T4> next, CancellationToken token = default);

}