namespace Interactions.Actions;

public delegate void Finally<in T>(T input);

public delegate ValueTask AsyncFinally<in T>(T input);