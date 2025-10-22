using Interactions.Builders;

namespace Interactions.Extensions;

public static class UseHandlersBuilderExtensions {

  public static Handler<T1, T2> End<T1, T2, T3, T4>(this UseHandlersBuilder<T1, T2, T3, T4> builder, Func<T3, T4> action) {
    return builder.End(Handler.FromMethod(action));
  }

  public static AsyncHandler<T1, T2> End<T1, T2, T3, T4>(
    this AsyncUseHandlersBuilder<T1, T2, T3, T4> builder, Func<T3, CancellationToken, ValueTask<T4>> action) {
    return builder.End(AsyncHandler.FromMethod(action));
  }

}