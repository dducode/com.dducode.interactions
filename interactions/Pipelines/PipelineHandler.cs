using Interactions.Core;

namespace Interactions.Pipelines;

internal sealed class PipelineHandler<T1, T2, T3, T4>(Pipeline<T1, T2, T3, T4> pipeline, Handler<T3, T4> next) : Handler<T1, T2> {

  public override T2 Handle(T1 input) {
    return pipeline.Invoke(input, next);
  }

}