namespace Interactions.Commands;

internal sealed class CompositeCommand<TIn> : Command<TIn> {

  private readonly IEnumerable<Command<TIn>> _commands;

  internal CompositeCommand(IEnumerable<Command<TIn>> commands) {
    _commands = commands;
  }

  public override bool Execute(TIn input) {
    return _commands.Aggregate(true, (current, command) => current & command.Execute(input));
  }

  public override IDisposable Handle(Handler<TIn, bool> handler) {
    throw new InvalidOperationException("Cannot handle composite command");
  }

}

internal sealed class AsyncCompositeCommand<TIn> : AsyncCommand<TIn> {

  private readonly IEnumerable<AsyncCommand<TIn>> _commands;

  internal AsyncCompositeCommand(IEnumerable<AsyncCommand<TIn>> commands) {
    _commands = commands;
  }

  public override async ValueTask<bool> Execute(TIn input, CancellationToken token = default) {
    var result = true;
    foreach (AsyncCommand<TIn> command in _commands)
      result &= await command.Execute(input, token);
    return result;
  }

  public override IDisposable Handle(AsyncHandler<TIn, bool> handler) {
    throw new InvalidOperationException("Cannot handle composite command");
  }

}

internal sealed class AsyncProxyCommand<TIn> : AsyncCommand<TIn> {

  private readonly Command<TIn> _command;

  internal AsyncProxyCommand(Command<TIn> command) {
    _command = command;
  }

  public override ValueTask<bool> Execute(TIn input, CancellationToken token = default) {
    return new ValueTask<bool>(!token.IsCancellationRequested && _command.Execute(input));
  }

  public override IDisposable Handle(AsyncHandler<TIn, bool> handler) {
    throw new InvalidOperationException("Cannot handle proxy command");
  }

}