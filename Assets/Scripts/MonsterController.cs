using Player;
using UnityEngine;

namespace Monster
{
    public class MonsterController : MonoBehaviour
    {
        #region 변수

        // 몬스터의 데이터
        public MonsterData Data { get; private set; }

        // 각 몬스터의 초기 데이터
        [SerializeField] private int _initialHP;
        [SerializeField] private int _initialATK;
        [SerializeField] private int _initialDEF;

        #endregion 변수

        #region 유니티 생명 주기 함수

        // Awake
        private void Awake()
        {
            // 데이터 클래스를 초기화하여 생성한다.
            Data = new(_initialHP, _initialATK, _initialDEF);
        }

        #endregion 유니티 생명 주기 함수

        #region 유니티 이벤트 함수

        // Trigger
        private void OnTriggerEnter(Collider other)
        {
            // 몬스터의 공격에 피격하면,
            if (other.TryGetComponent(out PlayerController player))
            {
                // 체력이 감소한다.
                Data.DamageHP(player.Data.ATK);
            }
        }

        #endregion 유니티 이벤트 함수

        #region 커스텀 함수

        // 플레이어의 데이터를 초기화한다.
        public void InitializeData()
        {
            // Data.HP = _initialHP;
            // Data.SP = _initialSP;
        }

        #endregion 커스텀 함수
    }
}
