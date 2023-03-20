using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("BattleView").GetComponent<BattleView>();
        Debug.Assert(View != null, "BattleView가 들어오지 않았습니다.");
        Presenter = new BattleViewPresenter();
        Debug.Assert(Presenter != null, "BattleViewPresenter가 들어오지 않았습니다.");
    }
}
