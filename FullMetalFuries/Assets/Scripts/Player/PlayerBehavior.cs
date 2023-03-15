using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Controller _controller;
    private Rigidbody2D _rigidbody;

    private const float MOVE_SPEED = 5f;

    void Start()
    {
        _controller = GetComponent<Controller>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_controller.x != 0 || _controller.y != 0)
        {
            Move();
        }

        if (_controller.attack)
        {
            // 공격
            Debug.Log("공격 ");
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
    }
}
