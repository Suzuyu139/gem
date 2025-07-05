using UnityEngine;
using R3;
using Cysharp.Threading.Tasks;

public class HomePresenter : PresenterBase
{
    private void Start()
    {
        Initialize().Forget();
    }

    async UniTask Initialize()
    {
        GameSceneManager.Instance.SetIsLoadComplete(true);

        IsInitialized = true;
        await UniTask.CompletedTask;
    }
}
