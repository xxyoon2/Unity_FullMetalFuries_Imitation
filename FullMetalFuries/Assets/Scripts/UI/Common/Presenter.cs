/// <summary>
/// 비즈니스 로직을 정의함 
/// </summary>
public abstract class Presenter
{
    /// <summary>
    /// View와 Presenter의 참조를 연결, View에 기본값을 할당
    /// 반드시 함수 종료 전 InitializeRx()를 호출해야 함
    /// </summary>
    /// <param name="view"></param>
    public abstract void OnInitialize(View view);

    /// <summary>
    /// ViewController가 파괴될 때 호출
    /// 자원 정리 용도
    /// </summary>
    public abstract void OnRelease();

    protected void InitializeRx()
    {
        OnOccuredUserEvent();
        OnUpdateModel();
    }

    /// <summary>
    /// View에 유저 이벤트가 발생했을 때 동작
    /// 보통 Model을 업데이트 
    /// </summary>
    protected abstract void OnOccuredUserEvent();

    /// <summary>
    /// Model이 업데이트 되었을 때 동작
    /// 보통 View를 업데이트
    /// </summary>
    protected abstract void OnUpdateModel();
}
