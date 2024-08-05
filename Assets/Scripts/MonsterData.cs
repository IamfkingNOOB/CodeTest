using System.ComponentModel;
using UnityEngine;

namespace Monster
{
    public class MonsterData
    {
        #region 변수 및 프로퍼티

        /// 몬스터의 현재 체력(HP)
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

        // 몬스터의 최대 체력
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

        // 몬스터의 공격력(ATK)
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

        // 몬스터의 방어력(DEF)
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

        #endregion 변수 및 프로퍼티

        #region 생성자

        // 생성자 시점에서 변수를 초기화한다.
        public MonsterData(int hp, int atk, int def)
        {
            _currentHP = _maxHP = hp; // 생성 시 현재 체력과 최대 체력은 같은 수치를 가진다.
            _atk = atk;
            _def = def;
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

        // 체력을 회복하는 함수; 최대 체력을 초과하여 회복할 수 없다.
        public void HealHP(int heal) => CurrentHP = Mathf.Min(CurrentHP + heal, MaxHP); // 고정값
        public void HealHP(float percentage) => HealHP(Mathf.FloorToInt(MaxHP * percentage)); // 비율값

        // 체력을 잃는 함수; 0 미만으로 잃을 수 없다.
        public void DamageHP(int damage) => CurrentHP = Mathf.Max(CurrentHP - damage, 0); // 고정값
        public void DamageHP(float percentage) => DamageHP(Mathf.FloorToInt(MaxHP * percentage)); // 회복값

        #endregion 커스텀 함수
    }
}
