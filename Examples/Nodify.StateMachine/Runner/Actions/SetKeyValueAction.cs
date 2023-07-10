using System.Threading.Tasks;

namespace Nodify.StateMachine.Runner.Actions;

[BlackboardItem("Set Value")]
public class SetKeyValueAction : IBlackboardAction
{
    [BlackboardProperty(BlackboardKeyType.Object)]
    public BlackboardProperty Key { get; set; }

    [BlackboardProperty(BlackboardKeyType.Object, CanChangeType = true)]
    public BlackboardProperty Value { get; set; }

    public Task Execute(Blackboard blackboard)
    {
        int? value = blackboard.GetValue<int>(Value);
        blackboard[Key] = value;

        return Task.CompletedTask;
    }
}
