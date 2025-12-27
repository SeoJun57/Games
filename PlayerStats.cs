using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Singleton
    private static PlayerStats s_Instance = null;

    public static PlayerStats Instance
    {
        get
        {
            if (s_Instance == null) return null;
            else { return s_Instance; }
        }
    }
    #endregion
    private void Awake()
    {
        #region Singleton
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
        _itemTrue = new bool[ItemData.items.Length];

    }
    public ItemSO ItemData;
    //아이템 길이 참조
    private bool[] _itemTrue;
    
    public bool[] ItemTrue
    {
        get { return _itemTrue; }
        set { _itemTrue = value; }
    }
    //총 준 데미지
    private int _totalDamage = 0;
    //플레이어 사망정보
    private bool _isDead = false;
    //플레이어의 위치 정보
    private Vector3 _playerTransform;
    //선택한 직업
    private int _select = 0;
    //레벨
    private int _level = 3;
    //공격력
    private int _baseAttackPower = 10;
    private int _bonusAttackPower = 0;
    //스피드
    private float _maxSpeed = 3;
    private float _speed = 3;
    private float _runSpeed = 7;
    //점프
    private float _jumpForce = 10f;
    private int _maxJump = 1;
    private int _jumpCount = 1;
    //골드
    private int _gold = 100;

    //체력, 스테미나
    private float _maxHp = 100;
    private float _currentHp = 100;

    private float _maxStamina = 100;
    private float _currentStamina = 100;
    //방어 유무
    private bool _isDefend = false;

    //스킬 관련
    private int _skillDamage = 50;
    private float _skillCost = 50;
    //private float _skillCoolDown = 5f; // TODO : 밸런스 안맞으면 로직 구현 필요
    private int _totalKills = 0;
    
    public int TotalKills
    {
        get { return _totalKills; }
        set { _totalKills = value; }
    }

    public int TotalDamage
    {
        get { return _totalDamage; }
        set { _totalDamage = value; }
    }

    public Vector3 PlayerTransform
    {
        get { return _playerTransform; }
        set { _playerTransform = value; }
    }
    public int SkillDamage
    {
        get { return _skillDamage; }
        set { _skillDamage = value; }
    }
    public float SkillCost
    {
        get { return _skillCost; }
        set { _skillCost = value; }
    }

    public int Select
    {
        get { return _select; }
        set { _select = value; }
    }
    public int Gold
    {
        get { return _gold; }
        set { _gold = value; }
    }
    public float RunSpeed
    {
        get { return _runSpeed; }
        set { _runSpeed = value; }
    }
    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public int AttackPower
    {
        get { return _baseAttackPower + _bonusAttackPower; }
       
    }
    public int BaseAttackPower
    {
        get { return _baseAttackPower; }
        set { _baseAttackPower = value; }
    }
    public int BonusAttackPower
    {
        get { return _bonusAttackPower; }
        set { _bonusAttackPower = value; }
    }
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public float MaxSpeed
    {
        get { return _maxSpeed; }
        set { _maxSpeed = value; }
    }
    public float JumpForce
    {
        get { return _jumpForce; }
    }
    public int MaxJump
    {
        get { return _maxJump; }
    }
    public int JumpCount
    {
        get { return _jumpCount; }
        set { _jumpCount = value; }
    }

    public float MaxHp
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }
    public float CurrentHp
    {
        get { return _currentHp; }
        set
        { 
            if(value  >= _maxHp)
            {
                _currentHp = _maxHp;
            }
            else if(value <= 0)
            {
                _currentHp = 0;
            }
            else
            {
                _currentHp = value;
            }
        }
    }
    public float MaxStamina
    {
        get { return _maxStamina; }
        set { _maxStamina = value; }
    }
    public float CurrentStamina
    {
        get { return _currentStamina; }
        set 
        { 
            if(value >= _maxStamina)
            {
                _currentStamina = _maxStamina;
            }
            else if(value <= 0)
            {
                _currentStamina = 0;
            }
            else
            {
                _currentStamina = value;
            }
        }
    }

    public bool IsDead
    {
        get { return _isDead; }
        set {  _isDead =  value; }
    }
    public bool IsDefend
    {
        get { return _isDefend; }
        set { _isDefend = value; }
    }
    bool itemAttackPower = true;
    private void Update()
    {
        if (_itemTrue[3])
        {
            if (itemAttackPower && _currentHp <= _maxHp / 2)
            {
                _bonusAttackPower += 20;
                itemAttackPower = false;
            }
            else if (itemAttackPower == false && _currentHp > _maxHp / 2)
            {
                _bonusAttackPower -= 20;
                itemAttackPower = true;
            }
        }
    }
}
