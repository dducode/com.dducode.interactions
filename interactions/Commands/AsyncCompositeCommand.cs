using Interactions.Core;
using Interactions.Core.Commands;

namespace Interactions.Commands;

internal sealed class AsyncCompositeCommand<T>(IEnumerable<AsyncCommand<T>> commands) : AsyncCommand<T> {

  public override async ValueTask<bool> Execute(T input, CancellationToken token = default) {
    var result = true;
    foreach (AsyncCommand<T> command in commands)
      result &= await command.Execute(input, token);
    return result;
  }

  public override IDisposable Handle(AsyncHandler<T, bool> handler) {
    throw new InvalidOperationException("Cannot handle composite command");
  }

}