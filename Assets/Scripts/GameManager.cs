using Player;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();

                if (instance == null)
                {
                    instance = new GameObject(typeof(T).Name, typeof(T)).GetComponent<T>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (transform.parent != null && transform.root != null)
        {
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}


public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private PlayerUI _playerUI;
    
    public PlayerController CurrentPlayer { get; set; }

    // 플레이어를 교체한다.
    public void ChangePlayerController(PlayerController newController)
    {
        _playerUI.ChangeController(newController);
    }

    public void Test()
    {
        PlayerData playerData = null; // DataManager를 통해 생성한 플레이어 데이터 중 하나를 골랐다고 가정.
        // 고른 데이터의 모델을 사용하여 게임 오브젝트를 생성하고, 고른 데이터를 참조시킨다.
        Instantiate(playerData.Model).AddComponent<PlayerController>().Data = playerData;
    }
}
