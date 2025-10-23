using System.Diagnostics.Contracts;
using Interactions.Actions;
using Interactions.Builders;
using Interactions.Core;
using Interactions.Handlers.Pipeline;

namespace Interactions;

public static partial class Pipeline<T1, T2> {

  [Pure]
  public static AsyncPipelineHandlersBuilder<T1, T2, T3, T4> Use<T3, T4>(AsyncPipeline<T1, T2, T3, T4> pipeline) {
    return new AsyncPipelineHandlersBuilder<T1, T2, T3, T4>(new AsyncAnonymousPipelineUnit<T1, T2, T3, T4>(pipeline));
  }

  [Pure]
  public static AsyncPipelineHandlersBuilder<T1, T2, T, T> Use<T>(AsyncPipeline<T1, T2, T, T> pipeline) {
    return Use<T, T>(pipeline);
  }

  [Pure]
  public static AsyncPipelineHandlersBuilder<T1, T2, T1, T2> Use(AsyncPipeline<T1, T2, T1, T2> pipeline) {
    return Use<T1, T2>(pipeline);
  }

  [Pure]
  public static AsyncPipelineHandlersBuilder<T1, T2, T, Unit> Use<T>(
    Func<T1, Func<T, CancellationToken, ValueTask>, CancellationToken, ValueTask<T2>> pipeline) {
    return Use<T, Unit>((input, next, token) => pipeline(input, async (i, t) => await next(i, t), token));
  }

  [Pure]
  public static AsyncPipelineHandlersBuilder<T1, T2, T2, Unit> Use(
    Func<T1, Func<T2, CancellationToken, ValueTask>, CancellationToken, ValueTask<T2>> pipeline) {
    return Use<T2, Unit>((input, next, token) => pipeline(input, async (i, t) => await next(i, t), token));
  }

  [Pure]
  public static AsyncPipelineHandlersBuilder<T1, T2, Unit, Unit> Use(
    Func<T1, Func<CancellationToken, ValueTask>, CancellationToken, ValueTask<T2>> pipeline) {
    return Use<Unit, Unit>((input, next, token) => pipeline(input, async t => await next(default, t), token));
  }

}

public static partial class Pipeline<T> {

  [Pure]
  public static AsyncPipelineHandlersBuilder<T, Unit, T1, Unit> Use<T1>(
    Func<T, Func<T1, CancellationToken, ValueTask>, CancellationToken, ValueTask> pipeline) {
    return new AsyncPipelineHandlersBuilder<T, Unit, T1, Unit>(new AsyncAnonymousPipelineUnit<T, Unit, T1, Unit>(async (input, next, token) => {
      await pipeline(input, async (i, t) => await next(i, t), token);
      return default;
    }));
  }

  [Pure]
  public static AsyncPipelineHandlersBuilder<T, Unit, T, Unit> Use(
    Func<T, Func<T, CancellationToken, ValueTask>, CancellationToken, ValueTask> pipeline) {
    return Use<T>(pipeline);
  }

  [Pure]
  public static AsyncPipelineHandlersBuilder<T, Unit, Unit, Unit> Use(
    Func<T, Func<CancellationToken, ValueTask>, CancellationToken, ValueTask> pipeline) {
    return Use<Unit>((input, next, token) => pipeline(input, t => next(default, t), token));
  }

}

public static partial class Pipeline {

  [Pure]
  public static AsyncPipelineHandlersBuilder<Unit, Unit, Unit, Unit> Use(
    Func<Func<CancellationToken, ValueTask>, CancellationToken, ValueTask> pipeline) {
    return new AsyncPipelineHandlersBuilder<Unit, Unit, Unit, Unit>(new AsyncAnonymousPipelineUnit<Unit, Unit, Unit, Unit>(async (_, next, token) => {
      await pipeline(async t => await next(default, t), token);
      return default;
    }));
  }

}