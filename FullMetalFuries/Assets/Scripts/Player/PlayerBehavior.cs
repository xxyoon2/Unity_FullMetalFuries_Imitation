using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Controller _controller;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private readonly string combinationKey = "attackCombination";
    private readonly string timeLimitKey = "attackTimeLimit";

    private const float MOVE_SPEED = 2f;

    [SerializeField] private int _hp = 100;

    private int attackCombination = 1;
    private float attackTimeLimit = 0;

    void Start()
    {
        _controller = GetComponent<Controller>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        GameManager.Instance.playerHit.AddListener(Hit);
    }

    void Update()
    {
        if (_controller.x != 0 || _controller.y != 0)
        {
            Move();
        }
        else
        {
            _animator.SetBool("isStop", true);
        }

        if (_controller.attack)
        {
            Attack();
        }

        //if (_controller.sec)
        //{
        //    // 보조기
        //    Debug.Log("보조 ");
        //}

        //if (_controller.evade)
        //{
        //    // 회피기
        //    Debug.Log("회피  ");
        //}

        //if (_controller.power)
        //{
        //    // 특수기
        //    Debug.Log("특수 ");
        //}
    }

    private void Move()
    {
        Vector2 playerPosition = transform.position;
        Vector2 point = playerPosition + Vector2.right * _controller.x + Vector2.up * _controller.y;
        _rigidbody.MovePosition(point + MOVE_SPEED * Time.deltaTime);
        _animator.SetBool("isStop", false);

        if (_controller.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void Attack()
    {
        Debug.Log("공격");

        _animator.SetInteger(combinationKey, attackCombination);
        _animator.SetTrigger("attack");

        // 파이터 일반 공격 구현
        // 1. 공격할 때 파이터가 보고있는 방향으로 조금씩 움직여야 함 (BehaviorState)
        // 1. 공격할 때는 키보드 입력으로 움직이는 것은 막아야함
        // 2. 공격 패턴이 있음 (총 5개의 일반 공격이 있음) 
        // 2-1. 피격이 없을 때는 1 - 2 반복
        // 2-2. 1번이건 2번이건 피격시키면 3 - 4 - 5 콤비네이션으로 넘어감
        // 일단 공격이라도 되게,


    }

    private void Hit(int damage)
    {
        Debug.Log("아야");
        _hp -= damage;

        if (_hp <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Debug.Log("사망");
    }
}
