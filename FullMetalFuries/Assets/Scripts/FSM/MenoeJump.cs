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

    private const float JUMP_HEIGHT = 20f;
    private const float SPEED = 5f;

    private Vector2 BezierCurve(float t)
    {
        float s = 1 - t;
        return (Mathf.Pow(s, 2) * _jumpingPoint) + (2 * s * t * _point) + (Mathf.Pow(t, 2) * _targetPoint);
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _jumpingPoint = _transform.position;
        _targetPoint = GameObject.FindWithTag("Player").transform.position;

        _point = new Vector2(_jumpingPoint.x, _jumpingPoint.y + JUMP_HEIGHT);

        _counter = 0f;
        animator.SetBool("isStop", false);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(_transform.position, _targetPoint);
        if (distance <= 0.1)
        {
            _transform.position = _targetPoint;
            animator.SetBool("isStop", true);
        }
        else
        {
            _counter += Time.deltaTime;
            Vector2 movePoint = BezierCurve(_counter);
            _transform.position = Vector2.MoveTowards(movePoint, movePoint, SPEED * Time.deltaTime);
        }
    }

    private const float ATTACK_RANGE_RADIUS = 3f;
    private const int PLAYER_LAYER = 1 << 7;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform attackRange = animator.transform.Find("RecoilRange");
        Collider2D collider = Physics2D.OverlapCircle(attackRange.position, ATTACK_RANGE_RADIUS, PLAYER_LAYER);
        if (collider != null)
        {
            // 데미지 입혔다고 알리기
        }
    }
}
