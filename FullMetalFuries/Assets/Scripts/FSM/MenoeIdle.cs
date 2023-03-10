using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeIdle : StateMachineBehaviour
{
    private const float WAITING_TIME = 2f;
    private const float TIME_OUT = 0f;

    private float _counter;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _counter = WAITING_TIME;
        animator.SetBool("isStop", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_counter <= TIME_OUT)
        {
            animator.SetBool("isStop", false);
        }

        _counter -= Time.deltaTime;
    }
}
