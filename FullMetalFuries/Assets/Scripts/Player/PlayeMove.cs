using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeMove : StateMachineBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Controller _controller;

    private const float MOVE_SPEED = 8f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _rigidbody = _transform.GetComponent<Rigidbody2D>();
        _controller = _transform.GetComponent<Controller>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 playerPosition = _transform.position;
        Vector2 point = playerPosition + (Vector2.right * _controller.x + Vector2.up * _controller.y) * MOVE_SPEED * Time.fixedDeltaTime;
        _rigidbody.MovePosition(point);

        if (_controller.x > 0)
        {
            _transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (_controller.x < 0)
        {
            _transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
