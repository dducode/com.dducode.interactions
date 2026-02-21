using System.Diagnostics.Contracts;
using Interactions.Builders;
using Interactions.Core;
using Interactions.Pipelines;

namespace Interactions.Extensions;

public static partial class PipelineBuilderExtensions {

  public static PipelineBuilder<T1, T2, T5, T6> Use<T1, T2, T3, T4, T5, T6>(
    this PipelineBuilder<T1, T2, T3, T4> builder,
    Func<T3, Func<T5, T6>, T4> pipeline) {
    return builder.Use(new AnonymousPipeline<T3, T4, T5, T6>((input, handler) => pipeline(input, handler.Handle)));
  }

  public static PipelineBuilder<T1, T2, T5, Unit> Use<T1, T2, T3, T4, T5>(
    this PipelineBuilder<T1, T2, T3, T4> builder,
    Func<T3, Action<T5>, T4> pipeline) {
    return builder.Use(new AnonymousPipeline<T3, T4, T5, Unit>((input, handler) => {
      return pipeline(input, i => handler.Handle(i));
    }));
  }

  public static PipelineBuilder<T1, T2, Unit, Unit> Use<T1, T2, T3, T4>(
    this PipelineBuilder<T1, T2, T3, T4> builder,
    Func<T3, Action, T4> pipeline) {
    return builder.Use(new AnonymousPipeline<T3, T4, Unit, Unit>((input, handler) => {
      return pipeline(input, () => handler.Handle(default));
    }));
  }

  public static PipelineBuilder<T1, T2, T4, T5> Use<T1, T2, T3, T4, T5>(
    this PipelineBuilder<T1, T2, T3, Unit> builder,
    Action<T3, Func<T4, T5>> pipeline) {
    return builder.Use(new AnonymousPipeline<T3, T4, T5>((input, handler) => pipeline(input, handler.Handle)));
  }

  public static PipelineBuilder<T1, T2, T4, Unit> Use<T1, T2, T3, T4>(
    this PipelineBuilder<T1, T2, T3, Unit> builder,
    Action<T3, Action<T4>> pipeline) {
    return builder.Use(new AnonymousPipeline<T3, T4, Unit>((input, handler) => {
      pipeline(input, i => handler.Handle(i));
    }));
  }

  public static PipelineBuilder<T1, T2, Unit, Unit> Use<T1, T2, T3>(
    this PipelineBuilder<T1, T2, T3, Unit> builder,
    Action<T3, Action> pipeline) {
    return builder.Use(new AnonymousPipeline<T3, Unit, Unit>((input, handler) => {
      pipeline(input, () => handler.Handle(default));
    }));
  }

  public static PipelineBuilder<T1, T2, Unit, Unit> Use<T1, T2>(
    this PipelineBuilder<T1, T2, Unit, Unit> builder,
    Action<Action> pipeline) {
    return builder.Use(new AnonymousPipeline<Unit, Unit, Unit>((_, handler) => {
      pipeline(() => handler.Handle(default));
    }));
  }

  [Pure]
  public static Handler<T1, T2> End<T1, T2, T3, T4>(this PipelineBuilder<T1, T2, T3, T4> builder, Func<T3, T4> action) {
    return builder.End(Handler.FromMethod(action));
  }

  [Pure]
  public static Handler<T1, T2> End<T1, T2, T3>(this PipelineBuilder<T1, T2, T3, Unit> builder, Action<T3> action) {
    return builder.End(Handler.FromMethod(action));
  }

  [Pure]
  public static Handler<T1, T2> End<T1, T2>(this PipelineBuilder<T1, T2, Unit, Unit> builder, Action action) {
    return builder.End(Handler.FromMethod(action));
  }

}