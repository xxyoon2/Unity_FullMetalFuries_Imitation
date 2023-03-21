using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounter : StateMachineBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Controller _controller;

    private const float MOVE_SPEED = 10f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _controller = _transform.GetComponent<Controller>();
        _rigidbody = _transform.GetComponent<Rigidbody2D>();
        _transform.GetComponent<PlayerBehavior>().SetState(PlayerBehavior.State.COUNTER);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool isCounterStrike = animator.GetBool("isCounterStrike");
        if (isCounterStrike)
        {
            Vector2 playerPosition = _transform.position;
            Vector2 point = playerPosition + (Vector2.right * _controller.x + Vector2.up * _controller.y) * MOVE_SPEED * Time.fixedDeltaTime;
            _rigidbody.MovePosition(point);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform.GetComponent<PlayerBehavior>().SetState(PlayerBehavior.State.NONE);
        animator.SetBool("isCounterStrike", false);
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
        
    //}

    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
        
    //}
}
