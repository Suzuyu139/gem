using UnityEngine;
using UnityEngine.SceneManagement;
using R3;

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
}
