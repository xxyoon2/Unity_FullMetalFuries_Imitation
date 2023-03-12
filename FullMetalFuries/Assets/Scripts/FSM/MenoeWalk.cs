using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeWalk : StateMachineBehaviour
{
    private Vector2 _pos;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private GameObject _target;

    private const float SPEED = 0.5f;
    private const float DISTANCE = 1f;
    private const float ATTACK_RANGE = 3f;

    enum SkillTree
    {
        NONE,
        AX,
        RUSHING,
        JUMP,
        STOMP,
        MAX,
    }

    private Vector2 SetDestination()
    {
        float x = Random.Range(_transform.position.x - DISTANCE, _transform.position.x + DISTANCE);
        float y = Random.Range(_transform.position.y - DISTANCE, _transform.position.y + DISTANCE);

        return new Vector2(x, y);
    }

    private int Attack()
    {
        return Random.Range((int)SkillTree.NONE, (int)SkillTree.MAX);
    }

    private bool CheckTargetLocation()
    {
        var targetPosition = GameObject.FindWithTag("Player").transform.position;
        var MenoePosition = _transform.position;
        float distance = (targetPosition - MenoePosition).sqrMagnitude;
        if (distance <= ATTACK_RANGE)
        {
            return true;
        }
        return false;
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _pos = SetDestination();
        _rigidbody = _transform.GetComponent<Rigidbody2D>();

        animator.SetInteger("attack", (int)SkillTree.NONE);

        Debug.Log("이동");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform.position = Vector2.MoveTowards(_transform.position, _pos, SPEED * Time.deltaTime);

        float distance = Vector2.Distance(_pos, _transform.position);
        if (distance <= 0.01f)
        {
            _pos = SetDestination();

            SkillTree skill = (SkillTree)Attack();
            switch (skill)
            {
                case SkillTree.AX:
                case SkillTree.RUSHING:
                case SkillTree.JUMP:
                case SkillTree.STOMP:
                    if (skill == SkillTree.AX && !CheckTargetLocation())
                    {
                        break;
                    }
                    Debug.Log("공격");
                    animator.SetInteger("attack", (int)skill);
                    return;
                default:
                    animator.SetBool("isStop", true);
                    break;
            }
        }
    }
}
