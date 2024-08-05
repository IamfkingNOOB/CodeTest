using System.ComponentModel;
using TMPro;
using UnityEngine;

namespace Player
{
    // 플레이어의 데이터를 UI로 다루는 클래스
    public class PlayerUI : MonoBehaviour
    {
        // 플레이어 클래스 (컨트롤러)
        private PlayerController _currentController;

        #region UI 요소

        [SerializeField] private TextMeshProUGUI _hpText; // 플레이어의 체력(HP) 문자
        [SerializeField] private TextMeshProUGUI _spText; // 플레이어의 기력(SP) 문자

        // 플레이어 데이터의 프로퍼티 변경 이벤트에 등록하는 콜백 함수로, 이벤트가 호출되면 UI의 요소가 변경된다.
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName) // 변경된 프로퍼티의 이름이
            {
                case nameof(_currentController.Data.CurrentHP): // 체력이라면,
                    _hpText.SetText($"{_currentController.Data.CurrentHP}"); // 체력 문자를 변경한다.
                    break;
                case nameof(_currentController.Data.CurrentSP): // 기력이라면,
                    _spText.SetText($"{_currentController.Data.CurrentSP}"); // 기력 문자를 변경한다.
                    break;
            }
        }

        #endregion UI 요소

        #region 유니티 생명 주기 함수

        // OnEnable
        private void OnEnable()
        {
            // 플레이어 클래스를 초기화한다.
            if (TryGetComponent(out _currentController))
            {
                // 플레이어 데이터의 프로퍼티 변경 이벤트에 콜백 함수를 등록한다.
                _currentController.Data?.AddPropertyChangedEventHandler(OnPropertyChanged);
            }
        }

        // OnDisable
        private void OnDisable()
        {
            // 플레이어 클래스를 초기화한다.
            if (TryGetComponent(out _currentController))
            {
                // 플레이어 데이터의 프로퍼티 변경 이벤트에 콜백 함수를 해제한다.
                _currentController.Data?.RemovePropertyChangedEventHandler(OnPropertyChanged);
            }
        }

        #endregion 유니티 생명 주기 함수

        #region 커스텀 함수

        // 플레이어를 교체할 때 호출하여, 다음 플레이어를 참조한다.
        public void ChangeController(PlayerController newController)
        {
            // 교체 전의 플레이어의 이벤트를 해제한다.
            _currentController.Data?.RemovePropertyChangedEventHandler(OnPropertyChanged);

            // 플레이어를 교체하고, 이벤트를 등록한다.
            _currentController = newController;
            _currentController.Data?.AddPropertyChangedEventHandler(OnPropertyChanged);
        }

        #endregion 커스텀 함수
    }
}
