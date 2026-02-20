using Interactions.Core;
using R3;
using Unit = Interactions.Core.Unit;

namespace Interactions.R3;

internal sealed class HandlerProxy<T>(Observer<T> inner) : Handler<T, Unit> {

  public override Unit Handle(T input) {
    inner.OnNext(input);
    return default;
  }

}