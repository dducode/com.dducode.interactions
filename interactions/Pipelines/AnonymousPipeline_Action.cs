using Interactions.Core;

namespace Interactions.Pipelines;

internal sealed class AnonymousPipeline_Action<T1, T2, T3>(Action<T1, Func<T2, T3>> pipeline) : Pipeline<T1, Unit, T2, T3> {

  public override Unit Invoke(T1 input, Handler<T2, T3> next) {
    pipeline(input, next.Handle);
    return default;
  }

}

internal sealed class AnonymousPipeline_Action<T1, T2>(Action<T1, Action<T2>> pipeline) : Pipeline<T1, Unit, T2, Unit> {

  public override Unit Invoke(T1 input, Handler<T2, Unit> next) {
    pipeline(input, i => next.Handle(i));
    return default;
  }

}

internal sealed class AnonymousPipeline_Action<T>(Action<T, Action> pipeline) : Pipeline<T, Unit, Unit, Unit> {

  public override Unit Invoke(T input, Handler<Unit, Unit> next) {
    pipeline(input, () => next.Handle(default));
    return default;
  }

}

internal sealed class AnonymousPipeline_Action(Action<Action> pipeline) : Pipeline<Unit, Unit, Unit, Unit> {

  public override Unit Invoke(Unit input, Handler<Unit, Unit> next) {
    pipeline(() => next.Handle(default));
    return default;
  }

}