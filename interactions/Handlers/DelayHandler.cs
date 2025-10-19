namespace Interactions.Handlers;

internal sealed class DelayHandler<T>(TimeSpan timeDelay) : AsyncHandler<T, T> {

  protected override async ValueTask<T> HandleCore(T input, CancellationToken token = default) {
    await Task.Delay(timeDelay, token);
    return input;
  }

}