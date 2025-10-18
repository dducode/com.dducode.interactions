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
  public static AsyncCommand<TIn> ToAsyncCommand<TIn>(this Command<TIn> command) {
    return new AsyncProxyCommand<TIn>(command);
  }

  [Pure]
  public static Command<TIn> Compose<TIn>(this Command<TIn> command, params Command<TIn>[] commands) {
    return new CompositeCommand<TIn>(Enumerable.Empty<Command<TIn>>().Append(command).Concat(commands));
  }

  [Pure]
  public static AsyncCommand<TIn> Compose<TIn>(this AsyncCommand<TIn> command, params AsyncCommand<TIn>[] commands) {
    return new AsyncCompositeCommand<TIn>(Enumerable.Empty<AsyncCommand<TIn>>().Append(command).Concat(commands));
  }

}