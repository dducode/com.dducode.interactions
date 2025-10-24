namespace Interactions.Pipelines;

public abstract class Pipeline<T1, T2, T3, T4> {

  protected internal abstract T2 Invoke(T1 input, Func<T3, T4> next);

}