using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskMove : Node
{
    private bool _waiting = true;
    private float _waitTime = 3f;
    private float _waitCounter = 0f;

    public TaskMove()
    {
        initialization();
    }

    private void initialization()
    {
        _waiting = true;
        _waitCounter = 0f;
        state = NodeState.FAILURE;
    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            // state = NodeState.FAILURE;
            Debug.Log($"이동, {state}");

            if (_waitCounter >= _waitTime)
            {
                _waiting = false;
                state = NodeState.SUCCESS;
            }
        }
        else
        {
            initialization();
        }

        return state;
    }
}
