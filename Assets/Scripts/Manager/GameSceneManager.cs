using UnityEngine;
using UnityEngine.SceneManagement;
using R3;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class GameSceneManager : MonoBehaviour
{
    static public GameSceneManager Instance { get; private set; }

    public enum LoadSceneType
    {
        Single,
        Additive,
    }

    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] float _animationSpeed = 1.0f;
    public float AnimationSpeed => _animationSpeed;

    public bool IsSceneChange { get; private set; } = false;

    bool _isLoadComplete = false;

    private void Awake()
    {
        Instance = this;
    }

    public void SetIsLoadComplete(bool isLoadComplete)
    {
        _isLoadComplete = isLoadComplete;
    }

    LoadSceneMode ConvertLoadSceneTypeToLoadSceneMode(LoadSceneType type)
    {
        LoadSceneMode mode = LoadSceneMode.Single;

        switch (type)
        {
            case LoadSceneType.Single:
                mode = LoadSceneMode.Single;
                break;

            case LoadSceneType.Additive:
                mode = LoadSceneMode.Additive;
                break;

            default:
                break;
        }

        return mode;
    }

    public void LoadScene(string sceneName, LoadSceneType type = LoadSceneType.Single)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            return;
        }

        SceneManager.LoadScene(sceneName, ConvertLoadSceneTypeToLoadSceneMode(type));
    }

    public async UniTask LoadSceneAsync(string sceneName, LoadSceneType type = LoadSceneType.Single)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            return;
        }

        await SceneManager.LoadSceneAsync(sceneName, ConvertLoadSceneTypeToLoadSceneMode(type));
    }

    public void LoadSceneChange(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            return;
        }

        FadeLoadScene(sceneName, LoadSceneMode.Single).Forget();
    }

    async UniTask FadeLoadScene(string sceneName, LoadSceneMode mode)
    {
        IsSceneChange = true;

        // DOTween フェードアウト
        await _canvasGroup.DOFade(1.0f, 1.0f / _animationSpeed).AsyncWaitForCompletion();

        // シーンロード（非同期）
        SceneManager.LoadScene(sceneName, mode);

        // ロード完了まで待機（呼び出し先で SetIsLoadComplete(true) を呼ぶこと）
        await UniTask.WaitUntil(() => _isLoadComplete);

        // フェードイン
        await _canvasGroup.DOFade(0.0f, 1.0f / _animationSpeed).AsyncWaitForCompletion();

        _isLoadComplete = false;
        IsSceneChange = false;
    }
}
