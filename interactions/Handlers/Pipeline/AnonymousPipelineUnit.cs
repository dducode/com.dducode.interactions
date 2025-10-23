using Interactions.Actions;

namespace Interactions.Handlers.Pipeline;

internal sealed class AnonymousPipelineUnit<T1, T2, T3, T4>(Pipeline<T1, T2, T3, T4> pipeline) : PipelineUnit<T1, T2, T3, T4> {

  protected internal override T2 Invoke(T1 input, Func<T3, T4> next) {
    return pipeline(input, next);
  }

}