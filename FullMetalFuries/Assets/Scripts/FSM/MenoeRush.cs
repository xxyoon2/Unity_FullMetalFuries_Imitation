using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeRush : StateMachineBehaviour
{
    private Transform _target;
    private Transform _Menoetius;

    private const float SPEED  = 1f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 플레이어 위치 받아옴
        _target = GameObject.FindWithTag("Player").transform;
        _Menoetius = animator.transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 플레이어로 향해 돌진
        // 공격 범위 내에 플레이어가 있다면 데미지
        
    }

    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
