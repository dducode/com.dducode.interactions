using System.Diagnostics.Contracts;
using Interactions.Core.Handlers;

namespace Interactions.Core.Extensions;

public static partial class BranchBuilderExtensions {

  public static BranchBuilder<T1, T2> ElseIf<T1, T2>(this BranchBuilder<T1, T2> builder, Func<bool> condition, Func<T1, T2> action) {
    return builder.ElseIf(condition, new AnonymousHandler_Func<T1, T2>(action));
  }

  public static BranchBuilder<Unit, T> ElseIf<T>(this BranchBuilder<Unit, T> builder, Func<bool> condition, Func<T> action) {
    return builder.ElseIf(condition, new AnonymousHandler_Func<T>(action));
  }

  public static BranchBuilder<T, Unit> ElseIf<T>(this BranchBuilder<T, Unit> builder, Func<bool> condition, Action<T> action) {
    return builder.ElseIf(condition, new AnonymousHandler_Action<T>(action));
  }

  public static BranchBuilder<Unit, Unit> ElseIf(this BranchBuilder<Unit, Unit> builder, Func<bool> condition, Action action) {
    return builder.ElseIf(condition, new AnonymousHandler_Action(action));
  }

  [Pure]
  public static Handler<T1, T2> Else<T1, T2>(this BranchBuilder<T1, T2> builder, Func<T1, T2> action) {
    return builder.Else(new AnonymousHandler_Func<T1, T2>(action));
  }

  [Pure]
  public static Handler<Unit, T> Else<T>(this BranchBuilder<Unit, T> builder, Func<T> action) {
    return builder.Else(new AnonymousHandler_Func<T>(action));
  }

  [Pure]
  public static Handler<T, Unit> Else<T>(this BranchBuilder<T, Unit> builder, Action<T> action) {
    return builder.Else(new AnonymousHandler_Action<T>(action));
  }

  [Pure]
  public static Handler<Unit, Unit> Else(this BranchBuilder<Unit, Unit> builder, Action action) {
    return builder.Else(new AnonymousHandler_Action(action));
  }

}