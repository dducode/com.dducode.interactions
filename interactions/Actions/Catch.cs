namespace Interactions.Actions;

public delegate T2 Catch<in TException, in T1, out T2>(TException exception, T1 input) where TException : Exception;

public delegate ValueTask<T2> AsyncCatch<in TException, in T1, T2>(TException exception, T1 input) where TException : Exception;