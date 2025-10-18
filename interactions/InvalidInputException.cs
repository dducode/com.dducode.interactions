namespace Interactions;

public class InvalidInputException : Exception {

  internal InvalidInputException(string message) : base(message) {
  }

}

public class InvalidOutputException : Exception {

  internal InvalidOutputException(string message) : base(message) {
  }

}