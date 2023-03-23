using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Scriptable Object/Stat", order = int.MaxValue)]
public class Stat : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name { get { return _name; } }
    [SerializeField] private int _hp = 100;
    public int HP { get { return _hp; } set { _hp = value; } }
    [SerializeField] private float _moveSpeed = 6f;
    public float MoveSpeed { get { return _moveSpeed; } }

    [Header("Primary Attack")]
    [SerializeField] private int _attackDamage;
    public int AttackDamage { get { return _attackDamage; } }
    [SerializeField] private float _attackMoveSpeed;
    public float AttackMoveSpeed { get { return _attackMoveSpeed; } }

    [Header("Secondary Skill")]
    [SerializeField] private int _secDamage;
    public int SecDamage { get { return _secDamage; } }
    [SerializeField] private float _secMoveSpeed;
    public float SecMoveSpeed { get { return _secMoveSpeed; } }

    [Header("Evade Ability")]
    [SerializeField] private int _evadeDamage;
    public int EvadeDamage { get { return _evadeDamage; } }
    [SerializeField] private float _evadeMoveSpeed;
    public float EvadeMoveSpeed { get { return _evadeMoveSpeed; } }

    [Header("Power Ability")]
    [SerializeField] private int _powerDamage;
    public int PowerDamage { get { return _powerDamage; } }
    [SerializeField] private float _powerMoveSpeed;
    public float PowerMoveSpeed { get { return _powerMoveSpeed; } }
}
