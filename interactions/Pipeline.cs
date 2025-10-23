using System.Diagnostics.Contracts;
using Interactions.Actions;
using Interactions.Builders;
using Interactions.Core;
using Interactions.Handlers.Pipeline;

namespace Interactions;

public static partial class Pipeline<T1, T2> {

  [Pure]
  public static PipelineHandlersBuilder<T1, T2, T3, T4> Use<T3, T4>(Pipeline<T1, T2, T3, T4> pipeline) {
    return new PipelineHandlersBuilder<T1, T2, T3, T4>(new AnonymousPipelineUnit<T1, T2, T3, T4>(pipeline));
  }

  [Pure]
  public static PipelineHandlersBuilder<T1, T2, T, T> Use<T>(Pipeline<T1, T2, T, T> pipeline) {
    return Use<T, T>(pipeline);
  }

  [Pure]
  public static PipelineHandlersBuilder<T1, T2, T1, T2> Use(Pipeline<T1, T2, T1, T2> pipeline) {
    return Use<T1, T2>(pipeline);
  }

  [Pure]
  public static PipelineHandlersBuilder<T1, T2, T, Unit> Use<T>(Func<T1, Action<T>, T2> pipeline) {
    return Use<T, Unit>((input, next) => pipeline(input, i => next(i)));
  }

  [Pure]
  public static PipelineHandlersBuilder<T1, T2, T2, Unit> Use(Func<T1, Action<T2>, T2> pipeline) {
    return Use<T2, Unit>((input, next) => pipeline(input, i => next(i)));
  }

  [Pure]
  public static PipelineHandlersBuilder<T1, T2, Unit, Unit> Use(Func<T1, Action, T2> pipeline) {
    return Use<Unit, Unit>((input, next) => pipeline(input, () => next(default)));
  }

}

public static partial class Pipeline<T> {

  [Pure]
  public static PipelineHandlersBuilder<T, Unit, T1, Unit> Use<T1>(Action<T, Action<T1>> pipeline) {
    return new PipelineHandlersBuilder<T, Unit, T1, Unit>(new AnonymousPipelineUnit<T, Unit, T1, Unit>((input, next) => {
      pipeline(input, i => next(i));
      return default;
    }));
  }

  [Pure]
  public static PipelineHandlersBuilder<T, Unit, T, Unit> Use(Action<T, Action<T>> pipeline) {
    return Use<T>(pipeline);
  }

  [Pure]
  public static PipelineHandlersBuilder<T, Unit, Unit, Unit> Use(Action<T, Action> pipeline) {
    return Use<Unit>((input, next) => pipeline(input, () => next(default)));
  }

}

public static partial class Pipeline {

  [Pure]
  public static PipelineHandlersBuilder<Unit, Unit, Unit, Unit> Use(Action<Action> pipeline) {
    return new PipelineHandlersBuilder<Unit, Unit, Unit, Unit>(new AnonymousPipelineUnit<Unit, Unit, Unit, Unit>((_, next) => {
      pipeline(() => next(default));
      return default;
    }));
  }

}