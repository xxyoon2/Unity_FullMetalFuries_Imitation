using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeRush : StateMachineBehaviour
{
    private Stat _stat;
    private Transform _Menoetius;
    private Vector3 _targetPosition;
    private Vector3 _dirVector;

    private const int PLAYER_LAYER = 1 << 7;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _stat = animator.transform.GetComponent<Menoetius>().stat;
        _targetPosition = GameObject.FindWithTag("Player").transform.position;
        _Menoetius = animator.transform;
        Vector3 _menoetiusPosition = _Menoetius.position;
        _dirVector = (_targetPosition - _menoetiusPosition).normalized;

        animator.SetBool("isRush", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 movePoint = new Vector3(_Menoetius.position.x, _Menoetius.position.y) + _dirVector * _stat.PowerMoveSpeed;
        movePoint.z = movePoint.y;
        _Menoetius.position = Vector2.MoveTowards(_Menoetius.position, movePoint, _stat.PowerMoveSpeed * Time.deltaTime);

        Transform attackRange = animator.transform.Find("AXAttackRange");
        Collider2D collider = Physics2D.OverlapBox(attackRange.transform.position, attackRange.transform.localScale, 0f, PLAYER_LAYER);
        if (collider != null)
        {
            GameManager.Instance.SufferDamage(_stat.PowerDamage);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isRush", false);
    }
}
