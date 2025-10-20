using System.Text;
using AutoFixture;
using Interactions.Commands;
using JetBrains.Annotations;
using Xunit.Abstractions;

namespace Interactions.Tests.Commands;

[TestSubject(typeof(ReversibleCommand<>))]
public class ReversibleCommandTest(ITestOutputHelper testOutputHelper) {

  [Fact]
  public void UndoRedoTest() {
    var fixture = new Fixture();
    var nums = new List<int>();

    var undoCommand = new Command<int>();
    undoCommand.Handle(Handler.AlwaysTrue<int>(num => nums.Remove(num)));

    var command = new ReversibleCommand<int>(undoCommand);
    command.Handle(Handler.AlwaysTrue<int>(num => nums.Add(num)));

    var builder = new StringBuilder();

    testOutputHelper.WriteLine("Start execute");

    for (var i = 0; i < 3; i++) {
      command.Execute(fixture.Create<int>());
      DisplayNumbers(nums, builder);
    }

    testOutputHelper.WriteLine("Start undo");
    while (command.Undo())
      DisplayNumbers(nums, builder);

    testOutputHelper.WriteLine("Start redo");
    while (command.Redo())
      DisplayNumbers(nums, builder);
  }

  private void DisplayNumbers(List<int> nums, StringBuilder builder) {
    for (var j = 0; j < nums.Count - 1; j++)
      builder.Append($"{nums[j]}; ");
    if (nums.Count > 0)
      builder.Append(nums.Last());
    else
      builder.Append(string.Empty);

    testOutputHelper.WriteLine($"Numbers: {builder}");
    builder.Clear();
  }

}