using Player;
using UnityEngine;
using UnityEngine.UI;

public class QTEButton : MonoBehaviour
{
    // 플레이어 클래스 (컨트롤러)
    private PlayerController _controller;

    #region UI 요소

    [SerializeField] private Button _qteButton;

    #endregion UI 요소

    #region 유니티 생명 주기 함수

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        _qteButton.onClick.AddListener(ChangeController);
    }

    private void OnDisable()
    {
        _qteButton.onClick.RemoveListener(ChangeController);
    }

    #endregion 유니티 생명 주기 함수

    #region 커스텀 함수

    private void ChangeController()
    {
        (_controller, GameManager.Instance.CurrentPlayer) = (GameManager.Instance.CurrentPlayer, _controller);
    }

    #endregion 커스텀 함수
}
