using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeRush : StateMachineBehaviour
{
    private Transform _Menoetius;
    private Vector2 _targetPosition;
    private Vector2 _dirVector;

    private const float SPEED  = 6f;
    private const float DURATION = 3f;

    private float _counter = 0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _targetPosition = GameObject.FindWithTag("Player").transform.position;
        _Menoetius = animator.transform;
        Vector2 _menoetiusPosition = _Menoetius.position;
        _dirVector = (_targetPosition - _menoetiusPosition).normalized;

        _counter = 0f;
        animator.SetBool("isRush", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_counter >= DURATION)
        {
            animator.SetBool("isRush", false);
        }

        Vector2 movePoint = new Vector2(_Menoetius.position.x, _Menoetius.position.y) + _dirVector * SPEED;
        _Menoetius.position = Vector2.MoveTowards(_Menoetius.position, movePoint, SPEED * Time.deltaTime);
        _counter += Time.deltaTime;
    }
}
