namespace Interactions.Extensions;

public static class StackExtensions {

  public static bool TryPop<T>(this Stack<T> stack, out T value) {
    if (stack.Count > 0) {
      value = stack.Pop();
      return true;
    }

    value = default;
    return false;
  }

}