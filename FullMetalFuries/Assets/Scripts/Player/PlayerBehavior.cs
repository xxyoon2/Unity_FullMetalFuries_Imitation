using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private Controller _controller;

    private const float SPEED = 5f;

    void Start()
    {
        _controller = GetComponent<Controller>();
    }

    void Update()
    {
        if (_controller.x != 0 || _controller.y != 0)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector2 playerPosition = transform.position;
        Vector2 point = playerPosition + Vector2.right * _controller.x + Vector2.up * _controller.y;
        transform.position = Vector2.MoveTowards(transform.position, point, SPEED * Time.deltaTime);
    }
}
