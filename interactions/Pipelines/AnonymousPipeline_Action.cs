using Interactions.Core;

namespace Interactions.Pipelines;

internal sealed class AnonymousPipeline_Action<T1, T2, T3>(Action<T1, Func<T2, T3>> pipeline) : Pipeline<T1, Unit, T2, T3> {

  protected internal override Unit Invoke(T1 input, Func<T2, T3> next) {
    pipeline(input, next);
    return default;
  }

}

internal sealed class AnonymousPipeline_Action<T1, T2>(Action<T1, Action<T2>> pipeline) : Pipeline<T1, Unit, T2, Unit> {

  protected internal override Unit Invoke(T1 input, Func<T2, Unit> next) {
    pipeline(input, i => next(i));
    return default;
  }

}

internal sealed class AnonymousPipeline_Action<T>(Action<T, Action> pipeline) : Pipeline<T, Unit, Unit, Unit> {

  protected internal override Unit Invoke(T input, Func<Unit, Unit> next) {
    pipeline(input, () => next(default));
    return default;
  }

}

internal sealed class AnonymousPipeline_Action(Action<Action> pipeline) : Pipeline<Unit, Unit, Unit, Unit> {

  protected internal override Unit Invoke(Unit input, Func<Unit, Unit> next) {
    pipeline(() => next(default));
    return default;
  }

}