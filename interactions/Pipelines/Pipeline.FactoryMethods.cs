using Interactions.Builders;
using Interactions.Core;

namespace Interactions.Pipelines;

public static partial class Pipeline<T1, T2> {

  public static PipelineBuilder<T1, T2, T3, T4> Use<T3, T4>(Func<T1, Func<T3, T4>, T2> pipeline) {
    return new PipelineBuilder<T1, T2, T3, T4>(new AnonymousPipeline_Func<T1, T2, T3, T4>(pipeline));
  }

  public static PipelineBuilder<T1, T2, T3, Unit> Use<T3>(Func<T1, Action<T3>, T2> pipeline) {
    return new PipelineBuilder<T1, T2, T3, Unit>(new AnonymousPipeline_Func<T1, T2, T3>(pipeline));
  }

  public static PipelineBuilder<T1, T2, Unit, Unit> Use(Func<T1, Action, T2> pipeline) {
    return new PipelineBuilder<T1, T2, Unit, Unit>(new AnonymousPipeline_Func<T1, T2>(pipeline));
  }

}

public static partial class Pipeline<T> {

  public static PipelineBuilder<T, Unit, T1, T2> Use<T1, T2>(Action<T, Func<T1, T2>> pipeline) {
    return new PipelineBuilder<T, Unit, T1, T2>(new AnonymousPipeline_Action<T, T1, T2>(pipeline));
  }

  public static PipelineBuilder<T, Unit, T1, Unit> Use<T1>(Action<T, Action<T1>> pipeline) {
    return new PipelineBuilder<T, Unit, T1, Unit>(new AnonymousPipeline_Action<T, T1>(pipeline));
  }

  public static PipelineBuilder<T, Unit, Unit, Unit> Use(Action<T, Action> pipeline) {
    return new PipelineBuilder<T, Unit, Unit, Unit>(new AnonymousPipeline_Action<T>(pipeline));
  }

}

public static partial class Pipeline {

  public static PipelineBuilder<Unit, Unit, Unit, Unit> Use(Action<Action> pipeline) {
    return new PipelineBuilder<Unit, Unit, Unit, Unit>(new AnonymousPipeline_Action(pipeline));
  }

}