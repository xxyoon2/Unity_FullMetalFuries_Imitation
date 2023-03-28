using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menoetius : MonoBehaviour
{
    [SerializeField] private Stat _stat;
    public Stat stat { get { return _stat; } }
    private Animator _animator;

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        initialization();

        GameManager.Instance.playerAttack.AddListener(Hit);
    }

    private void initialization()
    {
        _stat.HP = 200;
    }

    private void Hit(int damage)
    {
        _stat.HP -= damage;
        Model.Model.SetHp(_stat.HP);

        if (_stat.HP <= 0)
        {
            _animator.SetTrigger("death");
            GameManager.Instance.GameClear();
        }

        StartCoroutine("HitEffect");
    }

    IEnumerator HitEffect()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color existingColor = new Color(255f, 255f, 255f, 255f);
        Color transparent = new Color(255f, 0f, 0f, 255f);
        int counter = 0;
        while (counter < 3)
        {
            sprite.color = transparent;
            yield return new WaitForSeconds(0.02f);
            sprite.color = existingColor;
            yield return new WaitForSeconds(0.02f);

            ++counter;
        }
        yield break;
    }
}
