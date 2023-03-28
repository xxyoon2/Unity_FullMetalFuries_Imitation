using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleView : View
{
    public Slider bossHealthBar { get; private set; }

    void Awake()
    {
        bossHealthBar = transform.Find("BossHealthBar").GetComponent<Slider>();
        Debug.Assert(bossHealthBar != null, "체력바가 들어오지 않았습니다.");
    }
}
