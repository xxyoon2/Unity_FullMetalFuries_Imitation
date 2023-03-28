using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehavior<GameManager>
{
    public UnityEvent<int> playerAttack = new UnityEvent<int>();
    public UnityEvent<int> playerHit = new UnityEvent<int>();

    public void InflictDamage(int damage)
    {
        playerAttack.Invoke(damage);
    }

    public void SufferDamage(int damage)
    {
        playerHit.Invoke(damage);
    }

    public void GameOver()
    {
        Model.Model.SetEndingText("Game Over");
    }

    public void GameClear()
    {
        Model.Model.SetEndingText("Game Clear");
    }
}
