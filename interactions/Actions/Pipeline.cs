namespace Interactions.Actions;

public delegate T2 Pipeline<in T1, out T2, out T3, in T4>(T1 input, Func<T3, T4> next);

public delegate ValueTask<T2> AsyncPipeline<in T1, T2, out T3, T4>(
  T1 input, Func<T3, CancellationToken, ValueTask<T4>> next, CancellationToken token = default
);