namespace Interactions.Transformation.Parsing;

internal sealed class EnumParser<T> : Parser<T> where T : struct {

  internal static EnumParser<T> Instance { get; } = new();

  private EnumParser() {
  }

#if NET5_0_OR_GREATER
  protected override T Parse(string input) {
    return Enum.Parse<T>(input);
  }
#else
  protected override T Parse(string input) {
    return (T)Enum.Parse(typeof(T), input);
  }
#endif

}