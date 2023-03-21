using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeRush : StateMachineBehaviour
{
    private Transform _Menoetius;
    private Vector2 _targetPosition;
    private Vector2 _dirVector;

    private const float SPEED  = 6f;
    private const int DAMAGE = 10;
    private const int PLAYER_LAYER = 1 << 7;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _targetPosition = GameObject.FindWithTag("Player").transform.position;
        _Menoetius = animator.transform;
        Vector2 _menoetiusPosition = _Menoetius.position;
        _dirVector = (_targetPosition - _menoetiusPosition).normalized;

        animator.SetBool("isRush", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 movePoint = new Vector2(_Menoetius.position.x, _Menoetius.position.y) + _dirVector * SPEED;
        _Menoetius.position = Vector2.MoveTowards(_Menoetius.position, movePoint, SPEED * Time.deltaTime);

        Transform attackRange = animator.transform.Find("AXAttackRange");
        Collider2D collider = Physics2D.OverlapBox(attackRange.transform.position, attackRange.transform.localScale, 0f, PLAYER_LAYER);
        if (collider != null)
        {
            GameManager.Instance.SufferDamage(DAMAGE);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isRush", false);
    }
}
