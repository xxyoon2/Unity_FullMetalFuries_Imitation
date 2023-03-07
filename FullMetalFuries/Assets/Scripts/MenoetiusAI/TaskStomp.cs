using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskStomp : Node
{
    public TaskStomp()
    {

    }

    public override NodeState Evaluate()
    {
        if (Random.Range(0, 10) > 5)
        {
            Debug.Log("D_발구르기");
            state = NodeState.SUCCESS;
        }
        else
        {
            state = NodeState.FAILURE;
        }

        return state;
    }
}
