using Interactions.Builders;

namespace Interactions.Extensions;

public static class UseHandlersBuilderExtensions {

  public static Handler<T1, T2> End<T1, T2>(this UseHandlersBuilder<T1, T2> builder, Func<T1, T2> action) {
    return builder.End(Handler.FromMethod(action));
  }

  public static AsyncHandler<T1, T2> End<T1, T2>(this AsyncUseHandlersBuilder<T1, T2> builder, Func<T1, CancellationToken, ValueTask<T2>> action) {
    return builder.End(Handler.FromMethod(action));
  }

}