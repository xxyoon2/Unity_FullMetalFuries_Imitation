using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeAXAttack : StateMachineBehaviour
{
    private const int PLAYER_LAYER = 1 << 7;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform attackRange = animator.transform.Find("AXAttackRange");
        Collider2D collider = Physics2D.OverlapBox(attackRange.transform.position, attackRange.transform.localScale, 0f, PLAYER_LAYER);
        if (collider != null)
        {
            // 데미지 입혔다고 알리기
        }
    }
}
