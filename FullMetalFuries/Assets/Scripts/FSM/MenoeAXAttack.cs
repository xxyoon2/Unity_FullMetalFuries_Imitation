using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeAXAttack : StateMachineBehaviour
{
    private const int PLAYER_LAYER = 1 << 7;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Physics2D.Boxcast를 이용해 플레이어가 있는지 체크
        // - 있음 : 데미지 입혔다고 알림
        // - 없음 : 그냥 넘어가
        Transform attackRange = animator.transform.Find("AXAttackRange");
        Collider2D collider = Physics2D.OverlapBox(attackRange.transform.position, attackRange.transform.localScale, 0f, PLAYER_LAYER);
        if (collider != null)
        {
            // 데미지 입혔다고 알리기
        }
    }
}
