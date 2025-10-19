namespace Interactions.Analytics;

internal sealed class AnonymousMetrics<T1, T2>(
  Action<T1> call = null,
  Action<T2> success = null,
  Action<Exception> failure = null,
  Action<TimeSpan> latency = null) : IMetrics<T1, T2> {

  private readonly Action<T1> _call = call ?? delegate { };
  private readonly Action<T2> _success = success ?? delegate { };
  private readonly Action<Exception> _failure = failure ?? delegate { };
  private readonly Action<TimeSpan> _latency = latency ?? delegate { };

  public void Call(string tag, T1 input) {
    _call(input);
  }

  public void Success(string tag, T2 output) {
    _success(output);
  }

  public void Failure(string tag, Exception exception) {
    _failure(exception);
  }

  public void Latency(string tag, TimeSpan duration) {
    _latency(duration);
  }

}