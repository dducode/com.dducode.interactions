namespace Interactions.Core;

public readonly struct Result<T> : IEquatable<Result<T>> {

  public static Result<T> None => default;

  public readonly bool Success;
  public readonly Exception Exception;
  public T Value => Success ? _value : throw new InvalidOperationException("Cannot access value when result is not successful");

  private readonly T _value;

  private Result(T value) {
    Success = true;
    _value = value;
  }

  private Result(Exception exception) {
    Success = false;
    Exception = exception;
  }

  public static implicit operator Result<T>(T value) {
    return new Result<T>(value);
  }

  public static bool operator ==(Result<T> left, Result<T> right) {
    return left.Equals(right);
  }

  public static bool operator !=(Result<T> left, Result<T> right) {
    return !left.Equals(right);
  }

  public static Result<T> FromException(Exception exception) {
    return new Result<T>(exception);
  }

  public bool Equals(Result<T> other) {
    return Success == other.Success &&
           EqualityComparer<T>.Default.Equals(Value, other.Value) &&
           EqualityComparer<Exception>.Default.Equals(Exception, other.Exception);
  }

  public override bool Equals(object obj) {
    return obj is Result<T> other && Equals(other);
  }

  public override int GetHashCode() {
    return Success ? EqualityComparer<T>.Default.GetHashCode(Value) : EqualityComparer<Exception>.Default.GetHashCode(Exception);
  }

  public override string ToString() {
    return Success ? Value?.ToString() ?? "null" : Exception.Message;
  }

}