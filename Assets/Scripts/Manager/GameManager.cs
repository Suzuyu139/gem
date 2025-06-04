using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
