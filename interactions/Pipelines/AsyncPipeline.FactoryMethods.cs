using Interactions.Builders;
using Interactions.Core;

namespace Interactions.Pipelines;

public static partial class Pipeline<T1, T2> {

  public static AsyncPipelineBuilder<T1, T2, T3, T4> Use<T3, T4>(AsyncFunc<T1, AsyncFunc<T3, T4>, T2> pipeline) {
    return new AsyncPipelineBuilder<T1, T2, T3, T4>(new AsyncAnonymousPipeline_Func<T1, T2, T3, T4>(pipeline));
  }

  public static AsyncPipelineBuilder<T1, T2, T3, Unit> Use<T3>(AsyncFunc<T1, AsyncAction<T3>, T2> pipeline) {
    return new AsyncPipelineBuilder<T1, T2, T3, Unit>(new AsyncAnonymousPipeline_Func<T1, T2, T3>(pipeline));
  }

  public static AsyncPipelineBuilder<T1, T2, Unit, Unit> Use(AsyncFunc<T1, AsyncAction, T2> pipeline) {
    return new AsyncPipelineBuilder<T1, T2, Unit, Unit>(new AsyncAnonymousPipeline_Func<T1, T2>(pipeline));
  }

}

public static partial class Pipeline<T> {

  public static AsyncPipelineBuilder<T, Unit, T1, T2> Use<T1, T2>(AsyncAction<T, AsyncFunc<T1, T2>> pipeline) {
    return new AsyncPipelineBuilder<T, Unit, T1, T2>(new AsyncAnonymousPipeline_Action<T, T1, T2>(pipeline));
  }

  public static AsyncPipelineBuilder<T, Unit, T1, Unit> Use<T1>(AsyncAction<T, AsyncAction<T1>> pipeline) {
    return new AsyncPipelineBuilder<T, Unit, T1, Unit>(new AsyncAnonymousPipeline_Action<T, T1>(pipeline));
  }

  public static AsyncPipelineBuilder<T, Unit, Unit, Unit> Use(AsyncAction<T, AsyncAction> pipeline) {
    return new AsyncPipelineBuilder<T, Unit, Unit, Unit>(new AsyncAnonymousPipeline_Action<T>(pipeline));
  }

}

public static partial class Pipeline {

  public static AsyncPipelineBuilder<Unit, Unit, Unit, Unit> Use(AsyncAction<AsyncAction> pipeline) {
    return new AsyncPipelineBuilder<Unit, Unit, Unit, Unit>(new AsyncAnonymousPipeline_Action(pipeline));
  }

}