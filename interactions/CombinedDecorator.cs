namespace Interactions;

internal sealed class CombinedDecorator<T1, T2, T3> : Decorator<T1, T3> {

  private readonly Decorator<T1, T2> _first;
  private readonly Decorator<T2, T3> _second;

  internal CombinedDecorator(Decorator<T1, T2> first, Decorator<T2, T3> second) {
    _first = first;
    _second = second;
  }

  public override T3 Decorate(T1 item) {
    return _second.Decorate(_first.Decorate(item));
  }

}