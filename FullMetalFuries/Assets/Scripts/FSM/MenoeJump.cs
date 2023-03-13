using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeJump : StateMachineBehaviour
{
    private Transform _transform;
    private Vector3 _targetPoint;
    private Vector3 _jumpingPoint;

    private Vector3 _point1;
    private Vector3 _point2;

    private float _counter = 0f;

    private const float JUMP_TIME = 1f;
    private const float JUMP_HEIGHT = 10f;
    private const float SPEED = 5f;

    private Vector2 BezierCurve(float t)
    {
        float s = 1 - t;
        return (Mathf.Pow(s, 3) * _jumpingPoint) + (3 * Mathf.Pow(s, 2) * t * _point1) + (3 * s * Mathf.Pow(t, 2) * _point2) + (Mathf.Pow(t, 3) * _targetPoint);
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _jumpingPoint = _transform.position;
        _targetPoint = GameObject.FindWithTag("Player").transform.position;

        _point1 = new Vector2(_jumpingPoint.x, _jumpingPoint.y + JUMP_HEIGHT);
        _point2 = new Vector2(_targetPoint.x, _targetPoint.y + JUMP_HEIGHT);

        _counter = 0f;
        animator.SetBool("isStop", false);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_counter >= JUMP_TIME)
        {
            _transform.position = _jumpingPoint;
            animator.SetBool("isStop", true);
        }
        _counter += Time.deltaTime;
        Vector2 movePoint = BezierCurve(_counter);
        _transform.position = Vector2.MoveTowards(movePoint, movePoint, SPEED * Time.deltaTime);
    }
}
