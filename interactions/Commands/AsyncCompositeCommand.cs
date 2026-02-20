using Interactions.Core;
using Interactions.Core.Commands;
using Interactions.Extensions;

namespace Interactions.Commands;

internal sealed class AsyncCompositeCommand<T>(IEnumerable<AsyncCommand<T>> commands) : AsyncCommand<T> {

  public override async ValueTask<bool> Execute(T input, CancellationToken token = default) {
    return await commands.AggregateAsync(false, async (current, command, t) => current | await command.Execute(input, t), token);
  }

  public override IDisposable Handle(AsyncHandler<T, Unit> handler) {
    throw new NotSupportedException("Cannot handle composite command");
  }

}