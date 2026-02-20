namespace Interactions.Analytics;

internal sealed class AnonymousMetrics<T1, T2>(
  Action<T1> call = null,
  Action<T2> success = null,
  Action<Exception> failure = null,
  Action<TimeSpan> latency = null) : IMetrics<T1, T2> {

  public void Call(string tag, T1 input) {
    call?.Invoke(input);
  }

  public void Success(string tag, T2 output) {
    success?.Invoke(output);
  }

  public void Failure(string tag, Exception exception) {
    failure?.Invoke(exception);
  }

  public void Latency(string tag, TimeSpan duration) {
    latency?.Invoke(duration);
  }

}