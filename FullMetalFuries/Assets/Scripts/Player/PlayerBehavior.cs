using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Controller _controller;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private readonly string combinationKey = "attackCombination";

    private const float MOVE_SPEED = 8f;
    private int attackCombination = 1;

    [SerializeField] private int _hp = 100;
    [SerializeField] private State _state = State.NONE;

    private enum State
    {
        NONE,
        //ATTACK,     // 공격
        INVNC,      // 무적
        DEAD,       // 죽음
        MAX
    }

    void Start()
    {
        _controller = GetComponent<Controller>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        GameManager.Instance.playerHit.AddListener(Hit);
    }

    void FixedUpdate()
    {
        if (_state == State.DEAD)
        {
            return;
        }

        if (_controller.x != 0 || _controller.y != 0)
        {
            Move();
        }
        else
        {
            _animator.SetBool("isStop", true);
        }

        
    }

    void Update()
    {
        if (_state == State.DEAD)
        {
            return;
        }

        if (_controller.attack)
        {
            Attack();
        }
    }

    /// <summary>
    /// 플레이어 이동
    /// </summary>
    private void Move()
    {
        _animator.SetBool("isStop", false);
    }

    /// <summary>
    /// 플레이어 공격 
    /// </summary>
    private void Attack()
    {
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

    /// <summary>
    /// 적 공격에 맞았을 때 이벤트에 의해 실행되는 함수
    /// </summary>
    /// <param name="damage"></param>
    private void Hit(int damage)
    {
        if (_state == State.INVNC)
        {
            return;
        }

        _hp -= damage;

        if (_hp <= 0)
        {
            Dead();
        }

        _state = State.INVNC;
        StartCoroutine("InvincibleState");
    }

    /// <summary>
    /// 무적 상태
    /// </summary>
    /// <returns></returns>
    IEnumerator InvincibleState()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        int count = 0;
        while(count < 6)
        {
            sprite.color = new Color(255f, 255f, 255f, 0f);
            yield return new WaitForSeconds(0.2f);
            sprite.color = new Color(255f, 255f, 255f, 255f);
            yield return new WaitForSeconds(0.2f);

            ++count;
        }

        _state = State.NONE;
        yield break;
    }

    /// <summary>
    /// 플레이어가 체력이 0이 되었을 때 한 번 호출되는 함수 
    /// </summary>
    private void Dead()
    {
        GameManager.Instance.GameOver();

        _state = State.DEAD;
        _animator.SetTrigger("death");
    }
}
