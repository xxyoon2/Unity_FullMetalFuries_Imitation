using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskMove : Node
{
    public TaskMove()
    {

    }

    public override NodeState Evaluate()
    {
        Debug.Log("이동");
        state = NodeState.SUCCESS;
        return state;
    }
}
