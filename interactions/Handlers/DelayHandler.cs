namespace Interactions.Handlers;

internal sealed class DelayHandler<T>(Func<T, TimeSpan> delay) : AsyncHandler<T, T> {

  protected override async ValueTask<T> HandleCore(T input, CancellationToken token = default) {
    await Task.Delay(delay(input), token);
    return input;
  }

}