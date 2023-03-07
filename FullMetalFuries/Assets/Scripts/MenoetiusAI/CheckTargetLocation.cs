using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckTargetLocation : Node
{
    private UnityEngine.GameObject _target;
    private Transform _transform;

    public CheckTargetLocation(UnityEngine.GameObject target, Transform transform)
    {
        _target = target;
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        var targetPosition = _target.transform.position;
        var MenoePosition = _transform.position;
        float distance = (targetPosition - MenoePosition).sqrMagnitude;
        if (distance >= MenoetiusBT.attackRange)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
