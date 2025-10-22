using Interactions.Actions;
using Interactions.Core.Handlers;

namespace Interactions.Handlers;

internal sealed class AsyncPipelineHandler<T1, T2, T3, T4>(AsyncPipeline<T1, T2, T3, T4> func, AsyncHandler<T3, T4> next) : AsyncHandler<T1, T2> {

  protected internal override ValueTask<T2> Handle(T1 input, CancellationToken token = default) {
    return func(input, next.Handle, token);
  }

}