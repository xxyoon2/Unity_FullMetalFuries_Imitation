using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskJump : Node
{
    public TaskJump()
    {

    }

    public override NodeState Evaluate()
    {
        if (Random.Range(0, 10) > 5)
        {
            Debug.Log("B_도약");
            state = NodeState.SUCCESS;
        }
        else
        {
            state = NodeState.FAILURE;
        }

        return state;
    }
}
