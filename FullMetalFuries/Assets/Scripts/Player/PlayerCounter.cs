using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounter : StateMachineBehaviour
{
    private Stat _stat;

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Controller _controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _stat = _transform.GetComponent<Player>().stat;
        _controller = _transform.GetComponent<Controller>();
        _rigidbody = _transform.GetComponent<Rigidbody2D>();

        _transform.GetComponent<Player>().SetState(Player.State.COUNTER);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool isCounterStrike = animator.GetBool("isCounterStrike");
        if (isCounterStrike)
        {
            Vector2 playerPosition = _transform.position;
            Vector2 point = playerPosition + (Vector2.right * _controller.x + Vector2.up * _controller.y) * _stat.SecMoveSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(point);
            // 데미지도 줘야함 
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform.GetComponent<Player>().SetState(Player.State.NONE);
        animator.SetBool("isCounterStrike", false);
    }
}
