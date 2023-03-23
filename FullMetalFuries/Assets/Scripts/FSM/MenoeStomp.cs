using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeStomp : StateMachineBehaviour
{
    private const float ATTACK_RANGE_RADIUS = 3f;
    private const int PLAYER_LAYER = 1 << 7;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Stat stat = animator.transform.GetComponent<Menoetius>().stat;
        Transform attackRange = animator.transform.Find("RecoilRange");
        Collider2D collider = Physics2D.OverlapCircle(attackRange.position, ATTACK_RANGE_RADIUS, PLAYER_LAYER);
        if (collider != null)
        {
            GameManager.Instance.SufferDamage(stat.SecDamage);
        }

        // 폭격 관련 이벤트 알림
    }
}
