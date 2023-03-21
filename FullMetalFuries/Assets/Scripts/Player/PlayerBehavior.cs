using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Controller _controller;
    //private Rigidbody2D _rigidbody;
    private Animator _animator;

    private readonly string combinationKey = "attackCombination";

    private int _attackCombination = 1;

    [SerializeField] private int _hp = 100;
    [SerializeField] public State state { get; private set; }

    public enum State
    {
        NONE,
        COUNTER,
        JUMP,
        INVNC,      // 무적
        DEAD,       // 죽음
        MAX
    }

    void Start()
    {
        _controller = GetComponent<Controller>();
        //_rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        SetState(State.NONE);

        GameManager.Instance.playerHit.AddListener(Hit);
    }

    void FixedUpdate()
    {
        if (state == State.DEAD)
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
        if (state == State.DEAD)
        {
            return;
        }

        if (_controller.attack)
        {
            Attack();
        }

        if (_controller.sec)
        {
            SecSkill();
        }

        if (_controller.power)
        {
            PowerAbility();
        }

        if (_controller.evade)
        {
            EvadeAbility();
        }
    }

    public void SetState(State state)
    {
        this.state = state;
        Debug.Log($"{this.state}");
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

    private void SecSkill()
    {
        _animator.SetTrigger("counter");
    }

    private void PowerAbility()
    {
        _animator.SetTrigger("storm");
    }

    private void EvadeAbility()
    {
        if (state == State.JUMP)
        {
            return;
        }
        else
        {
            _animator.SetTrigger("jump");
            SetState(State.JUMP);

            StartCoroutine("Jumping");
        }
    }

    private const float JUMP_SPEED = 2f;
    IEnumerator Jumping()
    {
        while (_controller.evade)
        {
            yield return 0;
        }

        SetState(State.INVNC);
        _animator.SetBool("isJumping", true);

        Vector2 jumpingPoint = transform.position;
        Vector2 targetPoint = new Vector2(jumpingPoint.x + 5f, jumpingPoint.y);
        Vector2 point = new Vector2(jumpingPoint.x, jumpingPoint.y + 8f);

        float counter = 0f;

        while(counter <= 1f)
        {
            Vector2 movePoint = BezierCurve(counter, jumpingPoint, targetPoint, point);
            transform.position = Vector2.MoveTowards(movePoint, movePoint, JUMP_SPEED * Time.deltaTime);
            counter += 0.005f;

            if (!_animator.GetBool("isFalling") && counter >= 0.5f)
            {
                _animator.SetBool("isFalling", true);
            }

            yield return 0;
        }

        _animator.SetBool("isJumping", false);
        _animator.SetBool("isFalling", false);
        SetState(State.NONE);
        yield break;
    }

    private Vector2 BezierCurve(float t, Vector2 jumpingPoint, Vector2 targetPoint, Vector2 point)
    {
        float s = 1 - t;
        return (Mathf.Pow(s, 2) * jumpingPoint) + (2 * s * t * point) + (Mathf.Pow(t, 2) * targetPoint);
    }

    /// <summary>
    /// 적 공격에 맞았을 때 이벤트에 의해 실행되는 함수
    /// </summary>
    /// <param name="damage"></param>
    private void Hit(int damage)
    {
        if (state == State.INVNC)
        {
            return;
        }
        else if (state == State.COUNTER)
        {
            _animator.SetBool("isCounterStrike", true);
            return;
        }

        _hp -= damage;

        if (_hp <= 0)
        {
            Dead();
        }

        state = State.INVNC;
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

        state = State.NONE;
        yield break;
    }

    /// <summary>
    /// 플레이어가 체력이 0이 되었을 때 한 번 호출되는 함수 
    /// </summary>
    private void Dead()
    {
        GameManager.Instance.GameOver();

        state = State.DEAD;
        _animator.SetTrigger("death");
    }
}
