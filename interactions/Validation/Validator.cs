using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using Interactions.Extensions;

namespace Interactions.Validation;

public abstract class Validator<T> {

  public abstract string ErrorMessage { get; }

  protected internal abstract bool IsValid(T value);

}

public static class Validator {

  [Pure] public static Validator<int> ZeroEqual { get; } = Equal(0);
  [Pure] public static Validator<int> ZeroNotEqual { get; } = NotEqual(0);

  [Pure]
  public static Validator<string> NotEmptyString => NotNull<string>().And(StringLength(MoreThan(0)))
    .OverrideMessage("String cannot be null or empty");

  [Pure]
  public static Validator<T> Identity<T>() {
    return IdentityValidator<T>.Instance;
  }

  [Pure]
  public static Validator<T> NotNull<T>() where T : class {
    return NotNullValidator<T>.Instance;
  }

  [Pure]
  public static Validator<T> Equal<T>(T expected, IEqualityComparer<T> comparer = null) {
    return new EqualityValidator<T>(expected, comparer);
  }

  [Pure]
  public static Validator<T> NotEqual<T>(T expected, IEqualityComparer<T> comparer = null) {
    return Equal(expected, comparer).Inverse();
  }

  [Pure]
  public static Validator<T> MoreThan<T>(T value, IComparer<T> comparer = null) {
    return new MoreThanValidator<T>(value, comparer);
  }

  [Pure]
  public static Validator<T> LessThan<T>(T value, IComparer<T> comparer = null) {
    return new LessThanValidator<T>(value, comparer);
  }

  [Pure]
  public static Validator<T> MoreThanOrEqual<T>(T value, IComparer<T> comparer = null, IEqualityComparer<T> equalityComparer = null) {
    return MoreThan(value, comparer).Or(Equal(value, equalityComparer));
  }

  [Pure]
  public static Validator<T> LessThanOrEqual<T>(T value, IComparer<T> comparer = null, IEqualityComparer<T> equalityComparer = null) {
    return LessThan(value, comparer).Or(Equal(value, equalityComparer));
  }

  [Pure]
  public static Validator<T> InRange<T>(T min, T max, bool rightInclusive = false, IComparer<T> c = null, IEqualityComparer<T> ec = null) {
    return InRangeCore(min, max, rightInclusive, c, ec)
      .OverrideMessage($"Value must be in range [{min}..{max}{(rightInclusive ? "]" : ")")}");
  }

  [Pure]
  public static Validator<T> OutRange<T>(T min, T max, bool rightInclusive = false, IComparer<T> c = null, IEqualityComparer<T> ec = null) {
    return InRangeCore(min, max, rightInclusive, c, ec).Inverse()
      .OverrideMessage($"Value must be outside of range [{min}..{max}{(rightInclusive ? "]" : ")")}");
  }

  [Pure]
  public static Validator<string> StringLength(Validator<int> lengthValidator) {
    return new StringLengthValidator(lengthValidator);
  }

  [Pure]
  public static Validator<ICollection<T>> CollectionCount<T>(Validator<int> validator) {
    return new CollectionCountValidator<T>(validator);
  }

  [Pure]
  public static Validator<ICollection<T>> NotEmptyCollection<T>() {
    return CollectionCountValidator<T>.NotEmptyCollection;
  }

  [Pure]
  public static Validator<IEnumerable<T>> All<T>(Validator<T> itemValidator) {
    return new AllValidator<T>(itemValidator);
  }

  [Pure]
  public static Validator<IEnumerable<T>> Any<T>(Validator<T> itemValidator) {
    return new AnyValidator<T>(itemValidator);
  }

  [Pure]
  public static Validator<string> Regex(string pattern, RegexOptions options = RegexOptions.None) {
    return new RegexValidator(pattern, options);
  }

  [Pure]
  public static Validator<object> Type<TExpected>() {
    return TypeValidator<TExpected>.Instance;
  }

  [Pure]
  public static Validator<T> FromMethod<T>(Func<T, bool> validation, string errorMessage) {
    return new AnonymousValidator<T>(validation, errorMessage);
  }

  [Pure]
  private static Validator<T> InRangeCore<T>(T min, T max, bool rightInclusive = false, IComparer<T> c = null, IEqualityComparer<T> ec = null) {
    return MoreThan(min, c).Or(Equal(min, ec)).And(rightInclusive ? LessThan(max, c).Or(Equal(max, ec)) : LessThan(max, c));
  }

}