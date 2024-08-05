using System.ComponentModel;
using UnityEngine;

namespace Player
{
    // 플레이어의 데이터를 다루는 클래스
    public class PlayerData
    {
        #region 변수 및 프로퍼티

        // 플레이어의 현재 체력(HP)
        private int _currentHP;
        public int CurrentHP
        {
            get => _currentHP;
            private set
            {
                _currentHP = value;
                OnPropertyChanged(nameof(CurrentHP));
            }
        }

        // 플레이어의 최대 체력
        private int _maxHP;
        public int MaxHP
        {
            get => _maxHP;
            set
            {
                _maxHP = value;
                OnPropertyChanged(nameof(MaxHP));
            }
        }

        // 플레이어의 현재 기력(SP)
        private int _currentSP;
        public int CurrentSP
        {
            get => _currentSP;
            private set
            {
                _currentSP = value;
                OnPropertyChanged(nameof(CurrentSP));
            }
        }

        // 플레이어의 최대 기력
        private int _maxSP;
        public int MaxSP
        {
            get => _maxSP;
            private set
            {
                _maxSP = value;
                OnPropertyChanged(nameof(MaxSP));
            }
        }

        // 플레이어의 공격력(ATK)
        private int _atk;
        public int ATK
        {
            get => _atk;
            private set
            {
                _atk = value;
                OnPropertyChanged(nameof(ATK));
            }
        }

        // 플레이어의 방어력(DEF)
        private int _def;
        public int DEF
        {
            get => _def;
            private set
            {
                _def = value;
                OnPropertyChanged(nameof(DEF));
            }
        }

        // 플레이어의 회심(CRT)
        private int _crt;
        public int CRT
        {
            get => _crt;
            private set
            {
                _crt = value;
                OnPropertyChanged(nameof(CRT));
            }
        }

        // 필살기의 기력 소모량
        private int _ultimateCost;
        public int UltimateCost
        {
            get => _ultimateCost;
            private set
            {
                _ultimateCost = value;
                OnPropertyChanged(nameof(UltimateCost));
            }
        }

        // 기타 변수들…

        // 캐릭터의 모델 (프리팹)
        private GameObject _model;
        public GameObject Model
        {
            get => _model;
            set => _model = value;
        }

        #endregion 변수 및 프로퍼티

        #region 생성자

        // 생성자 시점에서 변수를 초기화한다.
        public PlayerData(int hp, int sp, int atk, int def, int crt, int ultimateCost)
        {
            _currentHP = _maxHP = hp; // 생성 시 현재 체력/기력과 최대 체력/기력은 같은 수치를 가진다.
            _currentSP = _maxSP = sp;
            _atk = atk;
            _def = def;
            _crt = crt;
            _ultimateCost = ultimateCost;
        }

        #endregion 생성자

        #region 프로퍼티 변경 이벤트 (Property Changed Event)

        // 프로퍼티 변경 이벤트
        private event PropertyChangedEventHandler PropertyChanged;

        // 이벤트를 호출하는 함수
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 이벤트에 핸들러(콜백 함수)를 등록하는 함수
        public void AddPropertyChangedEventHandler(PropertyChangedEventHandler handler)
        {
            PropertyChanged += handler;
        }

        // 이벤트에 핸들러(콜백 함수)를 해제하는 함수
        public void RemovePropertyChangedEventHandler(PropertyChangedEventHandler handler)
        {
            PropertyChanged -= handler;
        }

        #endregion 프로퍼티 변경 이벤트 (Property Changed Event)

        #region 커스텀 함수

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

        // 체력을 회복하는 함수; 최대 체력을 초과하여 회복할 수 없다.
        public void HealHP(int heal) => CurrentHP = Mathf.Min(CurrentHP + heal, MaxHP); // 고정값
        public void HealHP(float percentage) => HealHP(Mathf.FloorToInt(MaxHP * percentage)); // 비율값

        // 체력을 잃는 함수; 0 미만으로 잃을 수 없다.
        public void DamageHP(int damage) => CurrentHP = Mathf.Max(CurrentHP - damage, 0); // 고정값
        public void DamageHP(float percentage) => DamageHP(Mathf.FloorToInt(MaxHP * percentage)); // 회복값

        // 기력을 회복하는 함수; 최대 기력을 초과하여 회복할 수 없다.
        public void HealSP(int sp) => CurrentSP = Mathf.Min(CurrentSP + sp, MaxSP); // 고정값
        public void HealSP(float percentage) => HealSP(Mathf.FloorToInt(MaxSP * percentage)); // 비율값

        // 기력을 소모하는 함수; 현재 기력이 소모량 이상이어야 소모할 수 있다.
        public bool ConsumeSP(int sp)
        {
            if (CurrentSP >= sp)
            {
                CurrentSP -= sp;
                return true;
            }
            return false;
        }

        // 공격력을 증가시키는 함수
        public void IncreaseATK(int atk) => ATK += atk; // 고정값
        public void IncreaseATK(float percentage) => IncreaseATK(Mathf.FloorToInt(ATK * percentage)); // 비율값

        // 공격력을 감소시키는 함수
        public void DecreaseATK(int atk) => ATK -= atk; // 고정값
        public void DecreaseATK(float percentage) => DecreaseATK(Mathf.FloorToInt(ATK * percentage)); // 비율값

        // 방어력을 증가시키는 함수
        public void IncreaseDEF(int def) => DEF += def; // 고정값
        public void IncreaseDEF(float percentage) => IncreaseDEF(Mathf.FloorToInt(DEF * percentage)); // 비율값

        // 방어력을 감소시키는 함수
        public void DecreaseDEF(int def) => DEF -= def; // 고정값
        public void DecreaseDEF(float percentage) => DecreaseDEF(Mathf.FloorToInt(DEF * percentage)); // 비율값

        // 회심을 증가시키는 함수
        public void IncreaseCRT(int crt) => CRT += crt; // 고정값
        public void IncreaseCRT(float percentage) => IncreaseCRT(Mathf.FloorToInt(CRT * percentage)); // 비율값

        // 회심을 감소시키는 함수
        public void DecreaseCRT(int crt) => CRT -= crt; // 고정값
        public void DecreaseCRT(float percentage) => DecreaseCRT(Mathf.FloorToInt(CRT * percentage)); // 비율값

        #endregion 커스텀 함수
    }
}
