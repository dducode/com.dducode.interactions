namespace Interactions.Core;

public class MissingHandlerException : InvalidOperationException {

  internal MissingHandlerException(string message) : base(message) {
  }

}