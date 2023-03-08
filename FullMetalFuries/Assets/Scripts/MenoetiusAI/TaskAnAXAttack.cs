using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAnAXAttack : Node
{
    public TaskAnAXAttack()
    {

    }

    public override NodeState Evaluate()
    {
        if (Random.Range(0, 10) > 5)
        {
            Debug.Log("D_도끼휘두르기");
            state = NodeState.SUCCESS;
        }
        else
        {
            state = NodeState.FAILURE;
        }

        return state;
    }
}
