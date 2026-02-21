using Interactions.Core;

namespace Interactions.Pipelines;

internal sealed class AnonymousPipeline<T1, T2, T3, T4>(Func<T1, Handler<T3, T4>, T2> pipeline) : Pipeline<T1, T2, T3, T4> {

  public override T2 Invoke(T1 input, Handler<T3, T4> next) {
    return pipeline(input, next);
  }

}

internal sealed class AnonymousPipeline<T1, T2, T3>(Action<T1, Handler<T2, T3>> pipeline) : Pipeline<T1, Unit, T2, T3> {

  public override Unit Invoke(T1 input, Handler<T2, T3> next) {
    pipeline(input, next);
    return default;
  }

}