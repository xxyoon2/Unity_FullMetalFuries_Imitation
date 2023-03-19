using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menoetius : MonoBehaviour
{
    [SerializeField] private int _hp = 100;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();

        GameManager.Instance.playerAttack.AddListener(Hit);
    }

    private void Hit(int damage)
    {
        _hp -= damage;
        Model.BattleModel.SetHp(_hp);


        if (_hp <= 0)
        {
            _animator.SetTrigger("death");
            Debug.Log("미노 : 죽음");
        }
    }
}
