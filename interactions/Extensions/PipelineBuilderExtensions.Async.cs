using System.Diagnostics.Contracts;
using Interactions.Builders;
using Interactions.Core;
using Interactions.Pipelines;

namespace Interactions.Extensions;

public static partial class PipelineBuilderExtensions {

  public static AsyncPipelineBuilder<T1, T2, T5, T6> Use<T1, T2, T3, T4, T5, T6>(
    this AsyncPipelineBuilder<T1, T2, T3, T4> builder, AsyncFunc<T3, AsyncFunc<T5, T6>, T4> pipeline) {
    return builder.Use(new AsyncAnonymousPipeline_Func<T3, T4, T5, T6>(pipeline));
  }

  public static AsyncPipelineBuilder<T1, T2, T5, Unit> Use<T1, T2, T3, T4, T5>(
    this AsyncPipelineBuilder<T1, T2, T3, T4> builder, AsyncFunc<T3, AsyncAction<T5>, T4> pipeline) {
    return builder.Use(new AsyncAnonymousPipeline_Func<T3, T4, T5>(pipeline));
  }

  public static AsyncPipelineBuilder<T1, T2, Unit, Unit> Use<T1, T2, T3, T4>(
    this AsyncPipelineBuilder<T1, T2, T3, T4> builder, AsyncFunc<T3, AsyncAction, T4> pipeline) {
    return builder.Use(new AsyncAnonymousPipeline_Func<T3, T4>(pipeline));
  }

  public static AsyncPipelineBuilder<T1, T2, T4, T5> Use<T1, T2, T3, T4, T5>(
    this AsyncPipelineBuilder<T1, T2, T3, Unit> builder, AsyncAction<T3, AsyncFunc<T4, T5>> pipeline) {
    return builder.Use(new AsyncAnonymousPipeline_Action<T3, T4, T5>(pipeline));
  }

  public static AsyncPipelineBuilder<T1, T2, T4, Unit> Use<T1, T2, T3, T4>(
    this AsyncPipelineBuilder<T1, T2, T3, Unit> builder, AsyncAction<T3, AsyncAction<T4>> pipeline) {
    return builder.Use(new AsyncAnonymousPipeline_Action<T3, T4>(pipeline));
  }

  public static AsyncPipelineBuilder<T1, T2, Unit, Unit> Use<T1, T2, T3>(
    this AsyncPipelineBuilder<T1, T2, T3, Unit> builder, AsyncAction<T3, AsyncAction> pipeline) {
    return builder.Use(new AsyncAnonymousPipeline_Action<T3>(pipeline));
  }

  public static AsyncPipelineBuilder<T1, T2, Unit, Unit> Use<T1, T2>(
    this AsyncPipelineBuilder<T1, T2, Unit, Unit> builder, AsyncAction<AsyncAction> pipeline) {
    return builder.Use(new AsyncAnonymousPipeline_Action(pipeline));
  }

  [Pure]
  public static AsyncHandler<T1, T2> End<T1, T2, T3, T4>(this AsyncPipelineBuilder<T1, T2, T3, T4> builder, AsyncFunc<T3, T4> action) {
    return builder.End(Handler.FromMethod(action));
  }

  [Pure]
  public static AsyncHandler<T1, T2> End<T1, T2, T3>(this AsyncPipelineBuilder<T1, T2, T3, Unit> builder, AsyncAction<T3> action) {
    return builder.End(Handler.FromMethod(action));
  }

  [Pure]
  public static AsyncHandler<T1, T2> End<T1, T2>(this AsyncPipelineBuilder<T1, T2, Unit, Unit> builder, AsyncAction action) {
    return builder.End(Handler.FromMethod(action));
  }

}