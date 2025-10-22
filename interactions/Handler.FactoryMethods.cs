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

  public static UseHandlersBuilder<T1, T2, T3, T4> Use<T3, T4>(Use<T1, T2, T3, T4> func) {
    return new UseHandlersBuilder<T1, T2, T3, T4>(func);
  }

  public static UseHandlersBuilder<T1, T2, T, T> Use<T>(Use<T1, T2, T, T> func) {
    return Use<T, T>(func);
  }

  public static UseHandlersBuilder<T1, T2, T1, T2> Use(Use<T1, T2, T1, T2> func) {
    return Use<T1, T2>(func);
  }

}