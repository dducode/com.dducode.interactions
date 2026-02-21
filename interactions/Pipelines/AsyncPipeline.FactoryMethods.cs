using Interactions.Builders;
using Interactions.Core;

namespace Interactions.Pipelines;

public static partial class Pipeline<T1, T2> {

  public static AsyncPipelineBuilder<T1, T2, T3, T4> Use<T3, T4>(AsyncFunc<T1, AsyncHandler<T3, T4>, T2> pipeline) {
    return new AsyncPipelineBuilder<T1, T2, T3, T4>(new AsyncAnonymousPipeline<T1, T2, T3, T4>(pipeline));
  }

  public static AsyncPipelineBuilder<T1, T2, T3, T4> Use<T3, T4>(AsyncFunc<T1, AsyncFunc<T3, T4>, T2> pipeline) {
    return new AsyncPipelineBuilder<T1, T2, T3, T4>(new AsyncAnonymousPipeline<T1, T2, T3, T4>((input, handler, token) =>
      pipeline(input, handler.Handle, token)
    ));
  }

  public static AsyncPipelineBuilder<T1, T2, T3, Unit> Use<T3>(AsyncFunc<T1, AsyncAction<T3>, T2> pipeline) {
    return new AsyncPipelineBuilder<T1, T2, T3, Unit>(new AsyncAnonymousPipeline<T1, T2, T3, Unit>((input, handler, token) => {
      return pipeline(input, async (i, t) => await handler.Handle(i, t), token);
    }));
  }

  public static AsyncPipelineBuilder<T1, T2, Unit, Unit> Use(AsyncFunc<T1, AsyncAction, T2> pipeline) {
    return new AsyncPipelineBuilder<T1, T2, Unit, Unit>(new AsyncAnonymousPipeline<T1, T2, Unit, Unit>((input, handler, token) => {
      return pipeline(input, async t => await handler.Handle(default, t), token);
    }));
  }

}

public static partial class Pipeline<T> {

  public static AsyncPipelineBuilder<T, Unit, T1, T2> Use<T1, T2>(AsyncAction<T, AsyncHandler<T1, T2>> pipeline) {
    return new AsyncPipelineBuilder<T, Unit, T1, T2>(new AsyncAnonymousPipeline<T, T1, T2>(pipeline));
  }

  public static AsyncPipelineBuilder<T, Unit, T1, T2> Use<T1, T2>(AsyncAction<T, AsyncFunc<T1, T2>> pipeline) {
    return new AsyncPipelineBuilder<T, Unit, T1, T2>(new AsyncAnonymousPipeline<T, T1, T2>((input, handler, token) =>
      pipeline(input, handler.Handle, token)
    ));
  }

  public static AsyncPipelineBuilder<T, Unit, T1, Unit> Use<T1>(AsyncAction<T, AsyncAction<T1>> pipeline) {
    return new AsyncPipelineBuilder<T, Unit, T1, Unit>(new AsyncAnonymousPipeline<T, T1, Unit>((input, handler, token) => {
      return pipeline(input, async (i, t) => await handler.Handle(i, t), token);
    }));
  }

  public static AsyncPipelineBuilder<T, Unit, Unit, Unit> Use(AsyncAction<T, AsyncAction> pipeline) {
    return new AsyncPipelineBuilder<T, Unit, Unit, Unit>(new AsyncAnonymousPipeline<T, Unit, Unit>((input, handler, token) => {
      return pipeline(input, async t => await handler.Handle(default, t), token);
    }));
  }

}

public static partial class Pipeline {

  public static AsyncPipelineBuilder<Unit, Unit, Unit, Unit> Use(AsyncAction<AsyncAction> pipeline) {
    return new AsyncPipelineBuilder<Unit, Unit, Unit, Unit>(new AsyncAnonymousPipeline<Unit, Unit, Unit>((_, handler, token) => {
      return pipeline(async t => await handler.Handle(default, t), token);
    }));
  }

}