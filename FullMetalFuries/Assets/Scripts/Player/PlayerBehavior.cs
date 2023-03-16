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

        if (_controller.sec)
        {
            // 보조기
            Debug.Log("보조 ");
        }

        if (_controller.evade)
        {
            // 회피기
            Debug.Log("회피  ");
        }

        if (_controller.power)
        {
            // 특수기
            Debug.Log("특수 ");
        }
    }

    private void Move()
    {
        Vector2 playerPosition = transform.position;
        Vector2 point = playerPosition + Vector2.right * _controller.x + Vector2.up * _controller.y;
        _rigidbody.MovePosition(point + MOVE_SPEED * Time.deltaTime * Vector2.right);
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

        _animator.SetInteger(combinationKey, attackCombination % 2 + 1);
        _animator.SetTrigger("attack");
        attackCombination += 1;
        //GameManager.Instance.InflictDamage(5);
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
