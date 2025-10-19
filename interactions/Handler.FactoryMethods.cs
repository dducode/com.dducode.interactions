using Interactions.Builders;
using Interactions.Handlers;

namespace Interactions;

public abstract partial class Handler<T1, T2> {

  public static ConditionalHandlersBuilder<T1, T2> If(Func<bool> condition, Handler<T1, T2> handler) {
    return ConditionalHandlersBuilder<T1, T2>.If(condition, handler);
  }

  public static ConditionalHandlersBuilder<T1, T2> If(Func<bool> condition, Func<T1, T2> func) {
    return ConditionalHandlersBuilder<T1, T2>.If(condition, Handler.FromMethod(func));
  }

  public static UseHandlersBuilder<T1, T2> Use(Use<T1, T2> func) {
    return new UseHandlersBuilder<T1, T2>(func);
  }

}