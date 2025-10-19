using System.Diagnostics.Contracts;
using Interactions.Commands;

namespace Interactions.Extensions;

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

  [Pure]
  public static Command<T> Compose<T>(this Command<T> command, params Command<T>[] commands) {
    return new CompositeCommand<T>(Enumerable.Empty<Command<T>>().Append(command).Concat(commands));
  }

  [Pure]
  public static AsyncCommand<T> Compose<T>(this AsyncCommand<T> command, params AsyncCommand<T>[] commands) {
    return new AsyncCompositeCommand<T>(Enumerable.Empty<AsyncCommand<T>>().Append(command).Concat(commands));
  }

}