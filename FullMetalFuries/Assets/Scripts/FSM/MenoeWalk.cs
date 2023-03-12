using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeWalk : StateMachineBehaviour
{
    private Vector2 _pos;
    private Transform _transform;
    private Rigidbody2D _rigidbody;

    private const float SPEED = 1f;

    private Vector2 SetDestination()
    {
        float x = Random.Range(_transform.position.x - 3f, _transform.position.x + 3f);
        float y = Random.Range(_transform.position.y - 3f, _transform.position.y + 3f);

        return new Vector2(x, y);
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 랜덤 pos 정함
        _transform = animator.transform;
        _pos = SetDestination();
        _rigidbody = _transform.GetComponent<Rigidbody2D>();

        Debug.Log("이동");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 랜덤 pos로 이동
        // 만약 이동이 끝났다면 어떤 공격을 해야할 지 정해야함
        _transform.position = Vector2.MoveTowards(_transform.position, _pos, SPEED * Time.deltaTime);
        float distance = Vector2.Distance(_pos, _transform.position);

        if (distance <= 0.01f)
        {
            _transform.position = _pos;
            //int randomBehavior = Random.Range(0, 8);
            animator.SetBool("isStop", true);
            //switch (randomBehavior)
            //{
            //    case 1:
            //        Debug.Log("도끼");
            //        break;
            //    case 2:
            //        Debug.Log("돌진");
            //        break;
            //    case 3:
            //        Debug.Log("도약");
            //        break;
            //    case 4:
            //        Debug.Log("발");
            //        break;
            //    case 5:
            //        animator.SetBool("isStop", true);
            //        break;
            //    default:
            //        SetDestination();
            //        break;
            //}
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 이동이 끝났을 때 해야하는거...?
    }
}
