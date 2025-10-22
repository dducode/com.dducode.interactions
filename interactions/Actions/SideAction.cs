namespace Interactions.Actions;

public delegate void SideAction<in T>(T input);

public delegate ValueTask AsyncSideAction<in T>(T input, CancellationToken token = default);