using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : StateMachineBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Controller _controller;

    private float _x;
    private float _y;

    private const float MOVE_SPEED = 1f;
    private const int DAMAGE = 5;
    private const int ENEMY_LAYER = 1 << 8;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _rigidbody = _transform.GetComponent<Rigidbody2D>();
        _controller = _transform.GetComponent<Controller>();

        _x = _controller.x;
        _y = _controller.y;

        if (_controller.x > 0)
        {
            _transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            _transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        Transform attackRange = animator.transform.Find("AttackRange");
        Collider2D collider = Physics2D.OverlapBox(attackRange.transform.position, attackRange.transform.localScale, 0f, ENEMY_LAYER);
        if (collider != null)
        {
            _transform.GetComponent<PlayerBehavior>().HitSuces();
            Debug.Log("공격");

            GameManager.Instance.InflictDamage(DAMAGE);
        }

        _transform.GetComponent<PlayerBehavior>().SetCombinationCount();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 playerPosition = _transform.position;
        Vector2 point = playerPosition + (Vector2.right * _x + Vector2.up * _y) * MOVE_SPEED * Time.fixedDeltaTime;
        _rigidbody.MovePosition(point);
    }
}
