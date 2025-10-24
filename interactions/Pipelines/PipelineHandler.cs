using Interactions.Core;

namespace Interactions.Pipelines;

internal sealed class PipelineHandler<T1, T2, T3, T4>(Pipeline<T1, T2, T3, T4> unit, Handler<T3, T4> next) : Handler<T1, T2> {

  protected internal override T2 Handle(T1 input) {
    return unit.Invoke(input, next.Handle);
  }

}