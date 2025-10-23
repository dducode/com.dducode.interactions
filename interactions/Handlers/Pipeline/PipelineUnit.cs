using Interactions.Core;

namespace Interactions.Handlers.Pipeline;

public abstract class PipelineUnit<T1, T2, T3, T4> {

  protected internal abstract T2 Invoke(T1 input, Func<T3, T4> next);

}

public abstract class PipelineUnit<T1, T2> : PipelineUnit<T1, T2, T1, T2>;

public abstract class PipelineUnit<T> : PipelineUnit<T, T, T, T>;
public abstract class PipelineUnit : PipelineUnit<Unit, Unit, Unit, Unit>;