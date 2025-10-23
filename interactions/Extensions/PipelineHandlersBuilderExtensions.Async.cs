using System.Diagnostics.Contracts;
using Interactions.Builders;
using Interactions.Core;

namespace Interactions.Extensions;

public static partial class PipelineHandlersBuilderExtensions {

  [Pure]
  public static AsyncPipelineHandlersBuilder<T1, T2, T5, Unit> Use<T1, T2, T3, T4, T5>(
    this AsyncPipelineHandlersBuilder<T1, T2, T3, T4> builder,
    Func<T3, Func<T5, CancellationToken, ValueTask>, CancellationToken, ValueTask<T4>> pipeline) {
    return builder.Use<T5, Unit>((input, next, token) => pipeline(input, async (i, t) => await next(i, t), token));
  }

  [Pure]
  public static AsyncPipelineHandlersBuilder<T1, T2, T3, Unit> Use<T1, T2, T3, T4>(
    this AsyncPipelineHandlersBuilder<T1, T2, T3, T4> builder,
    Func<T3, Func<T3, CancellationToken, ValueTask>, CancellationToken, ValueTask<T4>> pipeline) {
    return builder.Use<T3, Unit>((input, next, token) => pipeline(input, async (i, t) => await next(i, t), token));
  }

  [Pure]
  public static AsyncPipelineHandlersBuilder<T1, T2, Unit, Unit> Use<T1, T2, T3, T4>(
    this AsyncPipelineHandlersBuilder<T1, T2, T3, T4> builder,
    Func<T3, Func<CancellationToken, ValueTask>, CancellationToken, ValueTask<T4>> pipeline) {
    return builder.Use<Unit>((input, next, token) => pipeline(input, async t => await next(default, t), token));
  }

  [Pure]
  public static AsyncPipelineHandlersBuilder<T1, Unit, T3, Unit> Use<T1, T2, T3>(
    this AsyncPipelineHandlersBuilder<T1, Unit, T2, Unit> builder,
    Func<T2, Func<T3, CancellationToken, ValueTask>, CancellationToken, ValueTask> pipeline) {
    return builder.Use<T3, Unit>(async (input, next, token) => {
      await pipeline(input, async (i, t) => await next(i, t), token);
      return default;
    });
  }

  [Pure]
  public static AsyncPipelineHandlersBuilder<T1, Unit, T2, Unit> Use<T1, T2>(
    this AsyncPipelineHandlersBuilder<T1, Unit, T2, Unit> builder,
    Func<T2, Func<T2, CancellationToken, ValueTask>, CancellationToken, ValueTask> pipeline) {
    return builder.Use<T1, T2, T2>(pipeline);
  }

  [Pure]
  public static AsyncPipelineHandlersBuilder<T1, Unit, Unit, Unit> Use<T1, T2>(
    this AsyncPipelineHandlersBuilder<T1, Unit, T2, Unit> builder,
    Func<T2, Func<CancellationToken, ValueTask>, CancellationToken, ValueTask> pipeline) {
    return builder.Use<T1, T2, Unit>((input, next, token) => pipeline(input, t => next(default, t), token));
  }

  [Pure]
  public static AsyncPipelineHandlersBuilder<Unit, Unit, Unit, Unit> Use(
    this AsyncPipelineHandlersBuilder<Unit, Unit, Unit, Unit> builder,
    Func<Func<CancellationToken, ValueTask>, CancellationToken, ValueTask> pipeline) {
    return builder.Use((_, next, token) => pipeline(t => next(default, t), token));
  }

  [Pure]
  public static AsyncHandler<T1, T2> End<T1, T2, T3, T4>(
    this AsyncPipelineHandlersBuilder<T1, T2, T3, T4> builder, Func<T3, CancellationToken, ValueTask<T4>> action) {
    return builder.End(AsyncHandler.FromMethod(action));
  }

  [Pure]
  public static AsyncHandler<T1, T2> End<T1, T2, T3>(
    this AsyncPipelineHandlersBuilder<T1, T2, T3, Unit> builder, Func<T3, CancellationToken, ValueTask> action) {
    return builder.End(AsyncHandler.FromMethod(action));
  }

  [Pure]
  public static AsyncHandler<T1, T2> End<T1, T2>(
    this AsyncPipelineHandlersBuilder<T1, T2, Unit, Unit> builder, Func<CancellationToken, ValueTask> action) {
    return builder.End(AsyncHandler.FromMethod(action));
  }

}