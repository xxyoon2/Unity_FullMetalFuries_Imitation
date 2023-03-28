using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeStomp : StateMachineBehaviour
{
    private const float ATTACK_RANGE_RADIUS = 4f;
    private const int PLAYER_LAYER = 1 << 7;

    private Transform _attackRange;
    private Stat _stat;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _stat = animator.transform.GetComponent<Menoetius>().stat;
        _attackRange = animator.transform.Find("RecoilRange");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D collider = Physics2D.OverlapCapsule(_attackRange.position, _attackRange.localScale, CapsuleDirection2D.Horizontal, 0f, PLAYER_LAYER);
        if (collider != null)
        {
            GameManager.Instance.SufferDamage(_stat.SecDamage);
        }
    }
}
