namespace Interactions.Core;

public readonly struct Optional<T> : IEquatable<Optional<T>> {

  public static Optional<T> None => default;

  public readonly bool HasValue;
  public T Value => HasValue ? _value : throw new InvalidOperationException("Cannot access value");

  private readonly T _value;

  private Optional(T value) {
    HasValue = true;
    _value = value;
  }

  public static implicit operator Optional<T>(T value) {
    return new Optional<T>(value);
  }

  public static bool operator ==(Optional<T> left, Optional<T> right) {
    return left.Equals(right);
  }

  public static bool operator !=(Optional<T> left, Optional<T> right) {
    return !left.Equals(right);
  }

  public bool Equals(Optional<T> other) {
    if (!HasValue && !other.HasValue)
      return true;
    return HasValue == other.HasValue && EqualityComparer<T>.Default.Equals(_value, other._value);
  }

  public override bool Equals(object obj) {
    return obj is Optional<T> other && Equals(other);
  }

  public override int GetHashCode() {
    return HasValue ? EqualityComparer<T>.Default.GetHashCode(_value) : 0;
  }

  public override string ToString() {
    return HasValue ? _value?.ToString() ?? "null" : "none";
  }

}