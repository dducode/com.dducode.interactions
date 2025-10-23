using Interactions.Core;

namespace Interactions.Handlers.Pipeline;

internal sealed class AsyncPipelineHandler<T1, T2, T3, T4>(AsyncPipelineUnit<T1, T2, T3, T4> unit, AsyncHandler<T3, T4> next) : AsyncHandler<T1, T2> {

  protected internal override ValueTask<T2> Handle(T1 input, CancellationToken token = default) {
    return unit.Invoke(input, next.Handle, token);
  }

}