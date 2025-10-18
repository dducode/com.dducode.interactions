namespace Interactions;

public static class Undo {

  public static Undo<TValue> FromResult<TValue>(TValue result) {
    return new Undo<TValue>(true, result);
  }

}

public readonly struct Undo<TValue>(bool success, TValue value) {

  public readonly bool success = success;
  public readonly TValue value = value;

}