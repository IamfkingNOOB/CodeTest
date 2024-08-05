using Monster;
using UnityEngine;

namespace Player
{
    // 플레이어의 조작을 다루는 클래스
    public class PlayerController : MonoBehaviour
    {
        #region 변수

        // 플레이어의 데이터
        public PlayerData Data { get; set; }

        // 각 플레이어의 초기 데이터
        [SerializeField] private int _initialHP; // 체력
        [SerializeField] private int _initialSP; // 기력
        [SerializeField] private int _initialATK; // 공격력
        [SerializeField] private int _initialDEF; // 방어력
        [SerializeField] private int _initialCRT; // 회심
        [SerializeField] private int _ultimateCost; // 필살기의 기력 소모량

        #endregion 변수

        #region 유니티 생명 주기 함수

        // Awake
        private void Awake()
        {
            // 데이터 클래스를 초기화하여 생성한다.
            Data = new(_initialHP, _initialSP, _initialATK, _initialDEF, _initialCRT, _ultimateCost);
        }

        #endregion 유니티 생명 주기 함수

        #region 유니티 이벤트 함수

        // Trigger
        private void OnTriggerEnter(Collider other)
        {
            // 몬스터의 공격에 피격하면,
            if (other.TryGetComponent(out MonsterController monster))
            {
                /*
                 * 대미지 공식
                 * 1. 대미지 증가: [공격력 * 스킬 계수 * ( 1 + 대미지 증가 + 대미지 증가 + … )]
                 * 2. 대미지 감소: [방어력 / (방어력 + 1000)]
                 *     - 방어력이 500일 경우: [500 / (500 + 1000)] ≒ 0.3333 = 약 33.33%의 대미지 감소
                 * 3. 회심 확률: [회심 / (75 + (레벨 * 5))]
                 *     - 회심이 100, 레벨이 80일 경우: [100 / (75 + (80 * 5))] ≒ 0.210 = 약 21%의 회심 확률
                 * 4. 회심 대미지: +100%
                 * 5. 상성: ±30%
                 */

                int damage = monster.Data.ATK * /* 몬스터의 스킬 계수 */ (1 /* + 대미지 증가… */); // 대미지 증가 공식 적용
                damage *= 1 - (Data.DEF / (Data.DEF + 1000)); // 대미지 감소 공식 적용

                // 체력이 감소한다.
                Data.DamageHP(damage);
            }
        }

        #endregion 유니티 이벤트 함수

        #region 커스텀 함수

        // 필살기
        private void UseUltimate()
        {
            // 기력이 소모량보다 많아 소모에 성공할 경우, (+ 재사용 대기시간이 아닐 경우,)
            if (Data.ConsumeSP(Data.UltimateCost))
            {
                // 필살기 상태에 진입한다.
                // playerState.ChangeState(new PlayerUltimateState(this));
            }
        }

        // 플레이어를 교체한다. ([TODO: 이 함수가 여기에 있는 게 적절한가?])
        private void ChangeController()
        {
            GameManager.Instance.ChangePlayerController(this);
        }

        #endregion 커스텀 함수
    }
}
