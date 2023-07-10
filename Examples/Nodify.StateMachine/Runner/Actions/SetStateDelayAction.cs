using System.Threading.Tasks;

namespace Nodify.StateMachine.Runner.Actions;

[BlackboardItem("Set State Delay")]
public class SetStateDelayAction : IBlackboardAction
{
    [BlackboardProperty("Delay", BlackboardKeyType.Integer)]
    public BlackboardProperty Delay { get; set; }

    [BlackboardProperty("Success", BlackboardKeyType.Boolean, Usage = BlackboardKeyUsage.Output)]
    public BlackboardProperty Success { get; set; }

    public Task Execute(Blackboard blackboard)
    {
        int? delay = blackboard.GetValue<int>(Delay);

        if (delay.HasValue)
        {
            blackboard[DebugBlackboardDecorator.StateDelayKey] = delay;
        }

        blackboard[Success] = delay.HasValue;

        return Task.CompletedTask;
    }
}
