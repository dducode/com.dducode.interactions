namespace Interactions.Core.Commands;

internal sealed class AsyncProxyCommand<T>(Command<T> command) : AsyncCommand<T> {

  public override ValueTask<bool> Execute(T input, CancellationToken token = default) {
    return new ValueTask<bool>(!token.IsCancellationRequested && command.Execute(input));
  }

  public override IDisposable Handle(AsyncHandler<T, Unit> handler) {
    throw new NotSupportedException("Cannot handle proxy command");
  }

}