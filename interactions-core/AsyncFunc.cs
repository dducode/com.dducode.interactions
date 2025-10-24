namespace Interactions.Core;

public delegate ValueTask<T3> AsyncFunc<in T1, in T2, T3>(T1 arg1, T2 arg2, CancellationToken token = default);

public delegate ValueTask<T2> AsyncFunc<in T1, T2>(T1 arg, CancellationToken token = default);

public delegate ValueTask<T> AsyncFunc<T>(CancellationToken token = default);