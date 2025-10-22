using Interactions.Core.Commands;
using Interactions.Core.Handlers;

namespace Interactions.Commands;

internal sealed class CompositeCommand<T>(IEnumerable<Command<T>> commands) : Command<T> {

  public override bool Execute(T input) {
    return commands.Aggregate(true, (current, command) => current & command.Execute(input));
  }

  public override IDisposable Handle(Handler<T, bool> handler) {
    throw new InvalidOperationException("Cannot handle composite command");
  }

}

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