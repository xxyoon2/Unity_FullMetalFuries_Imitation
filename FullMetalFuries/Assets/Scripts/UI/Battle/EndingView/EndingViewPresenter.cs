using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class EndingViewPresenter : Presenter
{
    private EndingView _endingView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();

    public override void OnInitialize(View view)
    {
        _endingView = view as EndingView;
        InitializeRx();
    }

    /// <summary>
    /// ViewController가 파괴될 때 호출
    /// 자원 정리 용도
    /// </summary>
    public override void OnRelease()
    {
        _endingView = default;
        _compositeDisposable.Dispose();
    }

    /// <summary>
    /// View에 유저 이벤트가 발생했을 때 동작
    /// 보통 Model을 업데이트 
    /// </summary>
    protected override void OnOccuredUserEvent()
    {

    }

    /// <summary>
    /// Model이 업데이트 되었을 때 동작
    /// 보통 View를 업데이트 
    /// </summary>
    protected override void OnUpdateModel()
    {
        Model.Model.endingText.Subscribe(Ending).AddTo(_compositeDisposable);
    }

    private void Ending(string text)
    {
        if (text == null)
        {
            return;
        }

        _endingView.background.gameObject.SetActive(true);
        _endingView.endingText.gameObject.SetActive(true);
        _endingView.endingText.text = text;
    }
}
