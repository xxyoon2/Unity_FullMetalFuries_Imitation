using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("EndingView").GetComponent<EndingView>();
        Debug.Assert(View != null);
        Presenter = new EndingViewPresenter();
        Debug.Assert(Presenter != null);
    }
}
