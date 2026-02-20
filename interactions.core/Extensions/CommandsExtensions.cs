using System.Diagnostics.Contracts;
using Interactions.Core.Commands;

namespace Interactions.Core.Extensions;

public static class CommandsExtensions {

  public static bool Execute(this Command<Unit> command) {
    return command.Execute(default);
  }

  public static ValueTask<bool> Execute(this AsyncCommand<Unit> command, CancellationToken token = default) {
    return command.Execute(default, token);
  }

  [Pure]
  public static AsyncCommand<T> ToAsyncCommand<T>(this Command<T> command) {
    return new AsyncProxyCommand<T>(command);
  }

}