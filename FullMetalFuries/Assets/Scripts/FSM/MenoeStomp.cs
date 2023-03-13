using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeStomp : StateMachineBehaviour
{
    private const float ATTACK_RANGE_RADIUS = 3f;
    private const int PLAYER_LAYER = 1 << 7;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform attackRange = animator.transform.Find("RecoilRange");
        Collider2D collider = Physics2D.OverlapCircle(attackRange.position, ATTACK_RANGE_RADIUS, PLAYER_LAYER);
        if (collider != null)
        {
            Debug.Log("으악1");
        }

        // 폭격 관련 이벤트 알림
    }
}
