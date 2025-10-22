using Interactions.Actions;
using Interactions.Builders;
using Interactions.Handlers;

namespace Interactions;

public static class Pipeline<T1, T2> {

  public static PipelineHandlersBuilder<T1, T2, T3, T4> Use<T3, T4>(Pipeline<T1, T2, T3, T4> func) {
    return new PipelineHandlersBuilder<T1, T2, T3, T4>(func);
  }

  public static PipelineHandlersBuilder<T1, T2, T, T> Use<T>(Pipeline<T1, T2, T, T> func) {
    return Use<T, T>(func);
  }

  public static PipelineHandlersBuilder<T1, T2, T1, T2> Use(Pipeline<T1, T2, T1, T2> func) {
    return Use<T1, T2>(func);
  }

  public static AsyncPipelineHandlersBuilder<T1, T2, T3, T4> Use<T3, T4>(AsyncPipeline<T1, T2, T3, T4> func) {
    return new AsyncPipelineHandlersBuilder<T1, T2, T3, T4>(func);
  }

  public static AsyncPipelineHandlersBuilder<T1, T2, T, T> Use<T>(AsyncPipeline<T1, T2, T, T> func) {
    return Use<T, T>(func);
  }

  public static AsyncPipelineHandlersBuilder<T1, T2, T1, T2> Use(AsyncPipeline<T1, T2, T1, T2> func) {
    return Use<T1, T2>(func);
  }

}