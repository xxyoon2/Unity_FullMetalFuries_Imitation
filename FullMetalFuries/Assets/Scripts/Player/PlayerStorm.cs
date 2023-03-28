using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorm : StateMachineBehaviour
{
    private Stat _stat;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Controller _controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _stat = _transform.GetComponent<Player>().stat;
        _rigidbody = _transform.GetComponent<Rigidbody2D>();
        _controller = _transform.GetComponent<Controller>();

        _counter = 0f;
    }

    private float _counter = 0f;
    private const float ATTACK_TIME = 0.5f;
    private const int ENEMY_LAYER = 1 << 8;
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 playerPosition = _transform.position;
        Vector2 point = playerPosition + (Vector2.right * _controller.x + Vector2.up * _controller.y) * _stat.PowerMoveSpeed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(point);

        if (_controller.x > 0)
        {
            _transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (_controller.x < 0)
        {
            _transform.localScale = new Vector3(-1f, 1f, 1f);
        }


        Transform attackRange = animator.transform.Find("StormRange");
        Collider2D collider = Physics2D.OverlapCapsule(attackRange.transform.position, attackRange.transform.localScale, CapsuleDirection2D.Horizontal, 0f, ENEMY_LAYER);
        _counter += Time.deltaTime;
        if (_counter >= ATTACK_TIME)
        {
            if (collider != null)
            {
                GameManager.Instance.InflictDamage(_stat.AttackDamage);
            }
            _counter = 0f;
        }
    }
}
