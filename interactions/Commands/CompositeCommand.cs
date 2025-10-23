using Interactions.Core;
using Interactions.Core.Commands;

namespace Interactions.Commands;

internal sealed class CompositeCommand<T>(IEnumerable<Command<T>> commands) : Command<T> {

  public override bool Execute(T input) {
    return commands.Aggregate(true, (current, command) => current & command.Execute(input));
  }

  public override IDisposable Handle(Handler<T, bool> handler) {
    throw new InvalidOperationException("Cannot handle composite command");
  }

}