using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeWalk : StateMachineBehaviour
{
    private Vector2 _pos;
    private Transform _transform;
    private Rigidbody2D _rigidbody;

    private const float SPEED = 1f;
    //private const float DISTANCE = 1f;
    private const float ATTACK_RANGE = 5f;
    private const float WAITING_TIME = 1.2f;
    private const float TIME_OUT = 0f;

    private float _counter;

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
        float x = Random.Range(-1, 2);
        float y = Random.Range(-1, 2);

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
        _rigidbody = _transform.GetComponent<Rigidbody2D>();
        _pos = SetDestination();

        animator.SetInteger("attack", (int)SkillTree.NONE);

        var targetPosition = GameObject.FindWithTag("Player").transform.position;
        if (targetPosition.x > _transform.position.x)
        {
            _transform.localScale = new Vector3(2f, 2f, 1f);
        }
        else
        {
            _transform.localScale = new Vector3(-2f, 2f, 1f);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 position = _transform.position;
        Vector2 point = position + _pos * SPEED * Time.fixedDeltaTime;
        _rigidbody.MovePosition(point);

        if (_counter <= TIME_OUT)
        {
            _pos = SetDestination();
            _counter = WAITING_TIME;

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
                    animator.SetInteger("attack", (int)skill);
                    return;
                default:
                    animator.SetBool("isStop", true);
                    break;
            }
        }

        _counter -= Time.deltaTime;
    }
}
