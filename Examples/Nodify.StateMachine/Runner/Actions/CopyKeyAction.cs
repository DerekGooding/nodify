﻿using System.Threading.Tasks;

namespace Nodify.StateMachine.Runner.Actions;

[BlackboardItem("Copy Key")]
public class CopyKeyAction : IBlackboardAction
{
    [BlackboardProperty("Source", BlackboardKeyType.Object)]
    public BlackboardProperty Source { get; set; }

    [BlackboardProperty("Target", BlackboardKeyType.Object)]
    public BlackboardProperty Target { get; set; }

    public Task Execute(Blackboard blackboard)
    {
        if (Source != Target && Source.IsKey && Target.IsKey)
        {
            object? value = blackboard[Source];
            blackboard[Target] = value;
        }

        return Task.CompletedTask;
    }
}
