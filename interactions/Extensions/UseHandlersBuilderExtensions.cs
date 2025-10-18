using Interactions.Builders;

namespace Interactions.Extensions;

public static class UseHandlersBuilderExtensions {

  public static Handler<TIn, TOut> End<TIn, TOut>(this UseHandlersBuilder<TIn, TOut> builder, Func<TIn, TOut> action) {
    return builder.End(Handler.FromMethod(action));
  }

  public static AsyncHandler<TIn, TOut> End<TIn, TOut>(
    this AsyncUseHandlersBuilder<TIn, TOut> builder, Func<TIn, CancellationToken, ValueTask<TOut>> action) {
    return builder.End(Handler.FromMethod(action));
  }

}