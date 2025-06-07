using UnityEngine;
using R3;

public class TitlePresenter : PresenterBase
{
    [SerializeField] TitleView _view;

    private void Start()
    {
        BindEvents();

        IsInitialized = true;
    }

    void BindEvents()
    {
        _view.OnScreenTapped.Subscribe(OnSceneChange).AddTo(gameObject);
    }

    void OnSceneChange(Unit unit)
    {
        // タップされたらシーン遷移する処理
        GameSceneManager.Instance.LoadSceneChange("Home");
    }
}
