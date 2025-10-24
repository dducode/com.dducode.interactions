using Interactions.Core;

namespace Interactions.Pipelines;

internal sealed class AnonymousPipeline_Func<T1, T2, T3, T4>(Func<T1, Func<T3, T4>, T2> pipeline) : Pipeline<T1, T2, T3, T4> {

  protected internal override T2 Invoke(T1 input, Func<T3, T4> next) {
    return pipeline(input, next);
  }

}

internal sealed class AnonymousPipeline_Func<T1, T2, T3>(Func<T1, Action<T3>, T2> pipeline) : Pipeline<T1, T2, T3, Unit> {

  protected internal override T2 Invoke(T1 input, Func<T3, Unit> next) {
    return pipeline(input, i => next(i));
  }

}

internal sealed class AnonymousPipeline_Func<T1, T2>(Func<T1, Action, T2> pipeline) : Pipeline<T1, T2, Unit, Unit> {

  protected internal override T2 Invoke(T1 input, Func<Unit, Unit> next) {
    return pipeline(input, () => next(default));
  }

}