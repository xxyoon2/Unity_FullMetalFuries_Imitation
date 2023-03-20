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
    private int _attackCombination = 1;

    [SerializeField] private int _hp = 100;
    [SerializeField] private State _state = State.NONE;

    private enum State
    {
        NONE,
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
        _animator.SetInteger(combinationKey, _attackCombination);
        _animator.SetTrigger("attack");
    }

    public void SetCombinationCount()
    {
        switch (_attackCombination)
        {
            case 1:
            case 2:
                if (_isCombinationPossible)
                {
                    _attackCombination = 3;
                }
                else
                {
                    _attackCombination = _attackCombination == 1 ? 2 : 1;
                }
                break;
            case 3:
            case 4:
                if (_isCombinationPossible)
                {
                    ++_attackCombination;
                }
                else
                {
                    _attackCombination = 1;
                }
                break;
            case 5:
                _attackCombination = 1;
                break;
            default:
                _attackCombination = 1;
                break;
        }
    }

    public void HitSuces()
    {
        StartCoroutine("HitDecisionCounter");
    }

    private bool _isCombinationPossible = false;
    private const float COMBINATION_COUNTER_TIME = 2f;

    IEnumerator HitDecisionCounter()
    {
        _isCombinationPossible = true;
        float counter = 0f;
        while (counter <= COMBINATION_COUNTER_TIME)
        {
            counter += Time.deltaTime;
            yield return 0;
        }

        _isCombinationPossible = false;
        yield break;
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
