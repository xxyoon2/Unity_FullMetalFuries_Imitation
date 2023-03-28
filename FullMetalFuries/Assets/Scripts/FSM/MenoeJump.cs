using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeJump : StateMachineBehaviour
{
    private Stat _stat;
    private Transform _transform;
    private Vector3 _targetPoint;
    private Vector3 _jumpingPoint;

    private Vector3 _highestPoint;

    private float _counter = 0f;

    private Vector3 BezierCurve(float t)
    {
        float s = 1 - t;
        return (Mathf.Pow(s, 2) * _jumpingPoint) + (2 * s * t * _highestPoint) + (Mathf.Pow(t, 2) * _targetPoint);
    }

    private const float JUMP_HEIGHT = 30f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _stat = _transform.GetComponent<Menoetius>().stat;
        _jumpingPoint = _transform.position;
        _targetPoint = GameObject.FindWithTag("Player").transform.position;

        Vector3 dir = (_jumpingPoint - _targetPoint).normalized;

        _highestPoint = _jumpingPoint + dir;
        _highestPoint.y += JUMP_HEIGHT;

        _counter = 0f;
        animator.SetBool("isStop", false);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_counter >= 1f)
        {
            _transform.position = _targetPoint;
            animator.SetBool("isStop", true);
        }
        else
        {
            _counter += Time.deltaTime;
            Vector3 movePoint = BezierCurve(_counter);
            movePoint.z = movePoint.y;
            _transform.position = Vector3.MoveTowards(movePoint, movePoint, _stat.EvadeDamage * Time.deltaTime);
        }
    }

    private const int PLAYER_LAYER = 1 << 7;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform attackRange = animator.transform.Find("RecoilRange");
        Collider2D collider = Physics2D.OverlapCapsule(attackRange.position, attackRange.localScale, CapsuleDirection2D.Horizontal, 0f, PLAYER_LAYER);
        if (collider != null)
        {
            GameManager.Instance.SufferDamage(_stat.SecDamage);
        }
    }
}
