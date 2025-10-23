using Interactions.Core;

namespace Interactions.Handlers.Pipeline;

public abstract class AsyncPipelineUnit<T1, T2, T3, T4> {

  protected internal abstract ValueTask<T2> Invoke(T1 input, Func<T3, CancellationToken, ValueTask<T4>> next, CancellationToken token = default);

}

public abstract class AsyncPipelineUnit<T1, T2> : AsyncPipelineUnit<T1, T2, T1, T2>;

public abstract class AsyncPipelineUnit<T> : AsyncPipelineUnit<T, T, T, T>;
public abstract class AsyncPipelineUnit : AsyncPipelineUnit<Unit, Unit, Unit, Unit>;