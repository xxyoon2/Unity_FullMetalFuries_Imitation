using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menoetius : MonoBehaviour
{
    [SerializeField] private int _hp = 100;

    void Start()
    {
        GameManager.Instance.playerAttack.AddListener(Hit);
    }

    private void Hit(int damage)
    {
        Debug.Log($"미노 : 아야 {_hp}");
        _hp -= damage;
    }
}
