using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenoeJump : StateMachineBehaviour
{
    private Transform _playerTransform;
    private Transform _transform;

    private float _distance;
    private float _velocity;
    private float _angle;

    private float _vX;
    private float _vY;

    private float _jumpTime;
    private float _counter = 0f;

    // 점프 높이
    private const float JUMP_HEIGHT = 10f;
    private const float JUMP_SPEED = 5f;

    //Vector3 GetVelocity(Vector3 currentPos, Vector3 targetPos, float initialAngle)
    //{
    //    float gravity = Physics.gravity.magnitude;
    //    float angle = initialAngle * Mathf.Deg2Rad;

    //    Vector3 planarTarget = new Vector3(targetPos.x, 0, targetPos.z);
    //    Vector3 planarPosition = new Vector3(currentPos.x, 0, currentPos.z);

    //    float distance = Vector3.Distance(planarTarget, planarPosition);
    //    float yOffset = currentPos.y - targetPos.y;

    //    float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

    //    Vector3 velocity = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

    //    float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (targetPos.x > currentPos.x ? 1 : -1);
    //    Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

    //    return finalVelocity;
    //}
    private Vector3 Parabola()
    {
        float f = -4 * JUMP_HEIGHT * _counter * _counter + 4 * JUMP_HEIGHT * _counter;
        var mid = Vector2.Lerp(_transform.position, _playerTransform.position, _counter);

        return new Vector2(mid.x, f + Mathf.Lerp(_transform.position.y, _playerTransform.position.y, _counter));
    }


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 플레이어 위치 받아오기
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _transform = animator.transform;

        _distance = Vector2.Distance(_playerTransform.position, _transform.position);
        _angle = Vector2.Angle(Vector2.zero, _playerTransform.position - _transform.position) * (_playerTransform.position.x > _transform.position.x ? 1 : -1);
        _velocity = _distance / (Mathf.Sin(2 * _angle * Mathf.Deg2Rad));

        _vX = Mathf.Sqrt(_velocity) * Mathf.Cos(_angle * Mathf.Deg2Rad);
        _vY = Mathf.Sqrt(_velocity) * Mathf.Sin(_angle * Mathf.Deg2Rad);

        _jumpTime = _distance / _vX;

        Debug.Log("점프");
        animator.SetBool("isJump", true);

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_transform.position.y >= _playerTransform.position.y)
        {
            _counter += Time.deltaTime;
            Vector3 tempPos = Parabola();
            _transform.position = tempPos;
        }
        else
        {
            animator.SetBool("isJump", false);
        }
        //_transform.Translate(_vX * Time.deltaTime, (_vY - _jumpTime) * Time.deltaTime, 0);
        //// 높이와 플레이어 위치 계산해서 점프 업데이트
        //if (_counter >= _jumpTime)
        //{
        //    animator.SetBool("isJump", false);
        //}
        //Debug.Log("점프 중");
        //_counter += Time.deltaTime;
    }
}
