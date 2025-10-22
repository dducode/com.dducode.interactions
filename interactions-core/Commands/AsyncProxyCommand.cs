using Interactions.Core.Handlers;

namespace Interactions.Core.Commands;

internal sealed class AsyncProxyCommand<T>(Command<T> command) : AsyncCommand<T> {

  public override ValueTask<bool> Execute(T input, CancellationToken token = default) {
    return new ValueTask<bool>(!token.IsCancellationRequested && command.Execute(input));
  }

  public override IDisposable Handle(AsyncHandler<T, bool> handler) {
    throw new InvalidOperationException("Cannot handle proxy command");
  }

}