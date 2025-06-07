using TMPro;
using UnityEngine;
using DG.Tweening;
using R3;
using UnityEngine.InputSystem;

public class TitleView : MonoBehaviour
{
    CompositeDisposable _disposables = new CompositeDisposable();

    [SerializeField] TextMeshProUGUI _tapToStart;

    Subject<Unit> _tapSubject = new Subject<Unit>();
    public Observable<Unit> OnScreenTapped => _tapSubject;

    bool _isOnScreenTapped = false;

    private void Start()
    {
        _disposables.Add(_tapSubject);

        TitleStartTextFadeAnimation();
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (_isOnScreenTapped)
        {
            return;
        }

        _isOnScreenTapped = true;
        _tapSubject.OnNext(Unit.Default);
    }

    void TitleStartTextFadeAnimation()
    {
        // アルファを0にしておく（透明）
        _tapToStart.alpha = 0f;

        // ループでフェードイン→フェードアウトを繰り返す
        _tapToStart.DOFade(1f, 1f)     // 1秒かけてフェードイン
            .SetLoops(-1, LoopType.Yoyo) // 無限ループ、フェードインとフェードアウトを交互に繰り返す
            .SetEase(Ease.InOutSine);   // イージングで自然なフェード
    }
}
