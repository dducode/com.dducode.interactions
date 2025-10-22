using Interactions.Builders;
using Interactions.Core.Handlers;

namespace Interactions.Extensions;

public static class UseHandlersBuilderExtensions {

  public static Handler<T1, T2> End<T1, T2, T3, T4>(this PipelineHandlersBuilder<T1, T2, T3, T4> builder, Func<T3, T4> action) {
    return builder.End(Handler.FromMethod(action));
  }

  public static AsyncHandler<T1, T2> End<T1, T2, T3, T4>(
    this AsyncPipelineHandlersBuilder<T1, T2, T3, T4> builder, Func<T3, CancellationToken, ValueTask<T4>> action) {
    return builder.End(AsyncHandler.FromMethod(action));
  }

}