using System.Diagnostics.Contracts;
using Interactions.Core.Handlers;

namespace Interactions.Core.Extensions;

public static partial class BranchBuilderExtensions {

  public static AsyncBranchBuilder<T1, T2> ElseIf<T1, T2>(this AsyncBranchBuilder<T1, T2> builder, Func<bool> condition, AsyncFunc<T1, T2> action) {
    return builder.ElseIf(condition, new AsyncAnonymousHandler_Func<T1, T2>(action));
  }

  public static AsyncBranchBuilder<Unit, T> ElseIf<T>(this AsyncBranchBuilder<Unit, T> builder, Func<bool> condition, AsyncFunc<T> action) {
    return builder.ElseIf(condition, new AsyncAnonymousHandler_Func<T>(action));
  }

  public static AsyncBranchBuilder<T, Unit> ElseIf<T>(this AsyncBranchBuilder<T, Unit> builder, Func<bool> condition, AsyncAction<T> action) {
    return builder.ElseIf(condition, new AsyncAnonymousHandler_Action<T>(action));
  }

  public static AsyncBranchBuilder<Unit, Unit> ElseIf(this AsyncBranchBuilder<Unit, Unit> builder, Func<bool> condition, AsyncAction action) {
    return builder.ElseIf(condition, new AsyncAnonymousHandler_Action(action));
  }

  [Pure]
  public static AsyncHandler<T1, T2> Else<T1, T2>(this AsyncBranchBuilder<T1, T2> builder, AsyncFunc<T1, T2> action) {
    return builder.Else(new AsyncAnonymousHandler_Func<T1, T2>(action));
  }

  [Pure]
  public static AsyncHandler<Unit, T> Else<T>(this AsyncBranchBuilder<Unit, T> builder, AsyncFunc<T> action) {
    return builder.Else(new AsyncAnonymousHandler_Func<T>(action));
  }

  [Pure]
  public static AsyncHandler<T, Unit> Else<T>(this AsyncBranchBuilder<T, Unit> builder, AsyncAction<T> action) {
    return builder.Else(new AsyncAnonymousHandler_Action<T>(action));
  }

  [Pure]
  public static AsyncHandler<Unit, Unit> Else(this AsyncBranchBuilder<Unit, Unit> builder, AsyncAction action) {
    return builder.Else(new AsyncAnonymousHandler_Action(action));
  }

}