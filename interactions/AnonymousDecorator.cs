namespace Interactions;

internal sealed class AnonymousDecorator<T1, T2> : Decorator<T1, T2> {

  private readonly Func<T1, T2> _decoration;

  internal AnonymousDecorator(Func<T1, T2> decoration) {
    _decoration = decoration;
  }

  public override T2 Decorate(T1 item) {
    return _decoration(item);
  }

}