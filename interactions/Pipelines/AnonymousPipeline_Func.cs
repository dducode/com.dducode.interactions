using Interactions.Core;

namespace Interactions.Pipelines;

internal sealed class AnonymousPipeline_Func<T1, T2, T3, T4>(Func<T1, Func<T3, T4>, T2> pipeline) : Pipeline<T1, T2, T3, T4> {

  public override T2 Invoke(T1 input, Handler<T3, T4> next) {
    return pipeline(input, next.Handle);
  }

}

internal sealed class AnonymousPipeline_Func<T1, T2, T3>(Func<T1, Action<T3>, T2> pipeline) : Pipeline<T1, T2, T3, Unit> {

  public override T2 Invoke(T1 input, Handler<T3, Unit> next) {
    return pipeline(input, i => next.Handle(i));
  }

}

internal sealed class AnonymousPipeline_Func<T1, T2>(Func<T1, Action, T2> pipeline) : Pipeline<T1, T2, Unit, Unit> {

  public override T2 Invoke(T1 input, Handler<Unit, Unit> next) {
    return pipeline(input, () => next.Handle(default));
  }

}