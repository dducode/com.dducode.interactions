using System.Text;

namespace Interactions.Transformation;

internal sealed class SplitConcatStringsTransformer(char[] separators) : SymmetricTransformer<string, IEnumerable<string>> {

  private readonly StringBuilder _sb = new();

  public override IEnumerable<string> Transform(string input) {
    return input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
  }

  public override string InverseTransform(IEnumerable<string> input) {
    _sb.Clear();
    foreach (string s in input)
      _sb.Append(s).Append(separators);
    _sb.Remove(_sb.Length - separators.Length, separators.Length);
    return _sb.ToString();
  }

}