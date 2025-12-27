using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsBar : MonoBehaviour
{
    //아이템 이미지가 생성될 위치
    public GameObject ItemSpawnPos;

    public Sprite[] PlayerSprites;

    //아이템 개수 카운트
    private int _itemCount = 0;
    public Image PlayerImage;
    public Image PlayerCurrentHpImage;
    public Image PlayerCurrentStaminaImage;

    public Text PlayerCurrentGold;
    public Text PlayerCurrentHpText;
    public Text PlayerCurrentStaminaText;

    public Text PlayerCurrentAttackDamageText;
    public Text PlayerCurrentSkillDamageText;
    public Text PlayerCurrentMaxHpText;
    public Text PlayerCurrentMaxStaminaText;
    public Text PlayerCurrentSpeedText;
    public Text PlayerCurrentRunSpeedText;

    public GameObject PlayerStatsPanal;
    public GameObject DeadPanal;

    public Image Items;
    bool isStats = false;
    #region Singleton
    private static PlayerStatsBar s_Instance = null;

    public static PlayerStatsBar Instance
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
    }
    private void Start()
    {
        
        PlayerImage.sprite = PlayerSprites[PlayerStats.Instance.Select];
    }
    private void Update()
    {
        PlayerStateUpdate();
        OnEquipment();
        StatsSet();
        if (PlayerStats.Instance.CurrentHp <= 0)
        {
            DeadPanal.SetActive(true);
        }
        else
        {
            DeadPanal.SetActive(false);
        }
    }

    void PlayerStateUpdate()
    {
        PlayerCurrentHpImage.fillAmount = PlayerStats.Instance.CurrentHp / PlayerStats.Instance.MaxHp;
        PlayerCurrentHpText.text = $"{PlayerStats.Instance.CurrentHp.ToString()} / {PlayerStats.Instance.MaxHp.ToString()}";
        PlayerCurrentStaminaImage.fillAmount = PlayerStats.Instance.CurrentStamina / PlayerStats.Instance.MaxStamina;
        PlayerCurrentStaminaText.text =$"{Mathf.FloorToInt( PlayerStats.Instance.CurrentStamina)} / {PlayerStats.Instance.MaxStamina }";
        PlayerCurrentGold.text = $"{PlayerStats.Instance.Gold}";
    }

    void OnEquipment()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            PlayerStatsPanal.SetActive(isStats = !isStats);        
        }        
    }
    void StatsSet()
    {
        PlayerCurrentAttackDamageText.text = PlayerStats.Instance.AttackPower.ToString();
        PlayerCurrentSkillDamageText.text = PlayerStats.Instance.SkillDamage.ToString();
        PlayerCurrentMaxHpText.text = PlayerStats.Instance.MaxHp.ToString();
        PlayerCurrentMaxStaminaText.text = PlayerStats.Instance.MaxStamina.ToString();
        PlayerCurrentSpeedText.text = PlayerStats.Instance.Speed.ToString();
        PlayerCurrentRunSpeedText.text = PlayerStats.Instance.RunSpeed.ToString();
    }
    //아이콘 추가 로직
    public void AddItem(Sprite sprite)
    {
        Vector2 pos = new Vector2(ItemSpawnPos.transform.position.x + (62 * _itemCount), ItemSpawnPos.transform.position.y);
        Image a =  Instantiate(Items, pos , Quaternion.identity, GameObject.Find("PlayerStatsBar").transform);        
        a.sprite = sprite;
        _itemCount++;
    }
}
