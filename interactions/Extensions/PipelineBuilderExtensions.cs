using System.Diagnostics.Contracts;
using Interactions.Builders;
using Interactions.Core;
using Interactions.Pipelines;

namespace Interactions.Extensions;

public static partial class PipelineBuilderExtensions {

  public static PipelineBuilder<T1, T2, T5, T6> Use<T1, T2, T3, T4, T5, T6>(
    this PipelineBuilder<T1, T2, T3, T4> builder, Func<T3, Func<T5, T6>, T4> pipeline) {
    return builder.Use(new AnonymousPipeline_Func<T3, T4, T5, T6>(pipeline));
  }

  public static PipelineBuilder<T1, T2, T5, Unit> Use<T1, T2, T3, T4, T5>(
    this PipelineBuilder<T1, T2, T3, T4> builder, Func<T3, Action<T5>, T4> pipeline) {
    return builder.Use(new AnonymousPipeline_Func<T3, T4, T5>(pipeline));
  }

  public static PipelineBuilder<T1, T2, Unit, Unit> Use<T1, T2, T3, T4>(
    this PipelineBuilder<T1, T2, T3, T4> builder, Func<T3, Action, T4> pipeline) {
    return builder.Use(new AnonymousPipeline_Func<T3, T4>(pipeline));
  }

  public static PipelineBuilder<T1, T2, T4, T5> Use<T1, T2, T3, T4, T5>(
    this PipelineBuilder<T1, T2, T3, Unit> builder, Action<T3, Func<T4, T5>> pipeline) {
    return builder.Use(new AnonymousPipeline_Action<T3, T4, T5>(pipeline));
  }

  public static PipelineBuilder<T1, T2, T4, Unit> Use<T1, T2, T3, T4>(
    this PipelineBuilder<T1, T2, T3, Unit> builder, Action<T3, Action<T4>> pipeline) {
    return builder.Use(new AnonymousPipeline_Action<T3, T4>(pipeline));
  }

  public static PipelineBuilder<T1, T2, Unit, Unit> Use<T1, T2, T3>(
    this PipelineBuilder<T1, T2, T3, Unit> builder, Action<T3, Action> pipeline) {
    return builder.Use(new AnonymousPipeline_Action<T3>(pipeline));
  }

  public static PipelineBuilder<T1, T2, Unit, Unit> Use<T1, T2>(
    this PipelineBuilder<T1, T2, Unit, Unit> builder, Action<Action> pipeline) {
    return builder.Use(new AnonymousPipeline_Action(pipeline));
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