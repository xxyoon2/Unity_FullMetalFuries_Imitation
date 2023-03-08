using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckTargetLocation : Node
{
    private Transform _transform;

    public CheckTargetLocation(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        var targetPosition = GameObject.FindWithTag("Player").transform.position;
        var MenoePosition = _transform.position;
        float distance = (targetPosition - MenoePosition).sqrMagnitude;
        float attackRange = _transform.GetComponent<MenoetiusBT>().attackRange;
        if (distance >= attackRange)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
