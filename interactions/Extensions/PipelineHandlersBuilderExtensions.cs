using System.Diagnostics.Contracts;
using Interactions.Builders;
using Interactions.Core;

namespace Interactions.Extensions;

public static partial class PipelineHandlersBuilderExtensions {

  [Pure]
  public static PipelineHandlersBuilder<T1, T2, T5, Unit> Use<T1, T2, T3, T4, T5>(
    this PipelineHandlersBuilder<T1, T2, T3, T4> builder, Func<T3, Action<T5>, T4> pipeline) {
    return builder.Use<T5, Unit>((input, next) => pipeline(input, i => next(i)));
  }

  [Pure]
  public static PipelineHandlersBuilder<T1, T2, T3, Unit> Use<T1, T2, T3, T4>(
    this PipelineHandlersBuilder<T1, T2, T3, T4> builder, Func<T3, Action<T3>, T4> pipeline) {
    return builder.Use<T3, Unit>((input, next) => pipeline(input, i => next(i)));
  }

  [Pure]
  public static PipelineHandlersBuilder<T1, T2, Unit, Unit> Use<T1, T2, T3, T4>(
    this PipelineHandlersBuilder<T1, T2, T3, T4> builder, Func<T3, Action, T4> pipeline) {
    return builder.Use<Unit>((input, next) => pipeline(input, () => next(default)));
  }

  [Pure]
  public static PipelineHandlersBuilder<T1, Unit, T3, Unit> Use<T1, T2, T3>(
    this PipelineHandlersBuilder<T1, Unit, T2, Unit> builder, Action<T2, Action<T3>> pipeline) {
    return builder.Use<T3, Unit>((input, next) => {
      pipeline(input, i => next(i));
      return default;
    });
  }

  [Pure]
  public static PipelineHandlersBuilder<T1, Unit, T2, Unit> Use<T1, T2>(
    this PipelineHandlersBuilder<T1, Unit, T2, Unit> builder, Action<T2, Action<T2>> pipeline) {
    return builder.Use<T1, T2, T2>(pipeline);
  }

  [Pure]
  public static PipelineHandlersBuilder<T1, Unit, Unit, Unit> Use<T1, T2>(
    this PipelineHandlersBuilder<T1, Unit, T2, Unit> builder, Action<T2, Action> pipeline) {
    return builder.Use<T1, T2, Unit>((input, next) => pipeline(input, () => next(default)));
  }

  [Pure]
  public static PipelineHandlersBuilder<Unit, Unit, Unit, Unit> Use(
    this PipelineHandlersBuilder<Unit, Unit, Unit, Unit> builder, Action<Action> action) {
    return builder.Use((_, next) => action(() => next(default)));
  }

  [Pure]
  public static Handler<T1, T2> End<T1, T2, T3, T4>(this PipelineHandlersBuilder<T1, T2, T3, T4> builder, Func<T3, T4> action) {
    return builder.End(Handler.FromMethod(action));
  }

  [Pure]
  public static Handler<T1, T2> End<T1, T2, T3>(this PipelineHandlersBuilder<T1, T2, T3, Unit> builder, Action<T3> action) {
    return builder.End(Handler.FromMethod(action));
  }

  [Pure]
  public static Handler<T1, T2> End<T1, T2>(this PipelineHandlersBuilder<T1, T2, Unit, Unit> builder, Action action) {
    return builder.End(Handler.FromMethod(action));
  }

}