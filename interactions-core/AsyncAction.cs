namespace Interactions.Core;

public delegate ValueTask AsyncAction<in T1, in T2>(T1 arg1, T2 arg2, CancellationToken token = default);

public delegate ValueTask AsyncAction<in T>(T arg, CancellationToken token = default);

public delegate ValueTask AsyncAction(CancellationToken token = default);