using Interactions.Core;

namespace Interactions.Pipelines;

public abstract class Pipeline<T1, T2, T3, T4> {

  public abstract T2 Invoke(T1 input, Handler<T3, T4> next);

}