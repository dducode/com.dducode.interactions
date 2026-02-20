using Interactions.Core;
using Interactions.Core.Commands;

namespace Interactions.Commands;

internal sealed class CompositeCommand<T>(IEnumerable<Command<T>> commands) : Command<T> {

  public override bool Execute(T input) {
    return commands.Aggregate(false, (current, command) => current | command.Execute(input));
  }

  public override IDisposable Handle(Handler<T, Unit> handler) {
    throw new NotSupportedException("Cannot handle composite command");
  }

}