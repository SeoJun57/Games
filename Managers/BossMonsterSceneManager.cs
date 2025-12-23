using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BossMonsterSceneManager : MonoBehaviour
{
    Transform player;

    public BossStatsSO BossData;
    
    public GameObject Lv3Platform;

    public GameObject[] Player;
    public GameObject[] BossMonster;
    public GameObject BossMonsterHpBar;
    public GameObject ClearPanal;
    public GameObject GameClearPanal;
    public Image BossCurrentHpImage;

    public Image ClearImage;
    public Text TotalDamageText;
    public Text TotalKillText;

    public Button MaxHpUpgrade;
    public Button AttackPowerUpgrade;

    public Button EndButton;
    public Button NextButton;

    private float _currentHp;
    private float _maxHp;

    public float CurrentHp
    {
        get { return _currentHp; }
        set { _currentHp = value; }
    }
    public float MaxHp
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }
  
    public static int BossMonsterCount = 1;

    void Start()
    {
        BossMonsterCount = 1;
        Time.timeScale = 1;
        MaxHpUpgrade.onClick.AddListener(OnClickMaxHpUpgrade);
        AttackPowerUpgrade.onClick.AddListener(OnClickAttackPowerUpgrade);
        NextButton.onClick.AddListener(OnClickNextButton);
        EndButton.onClick.AddListener(OnClickEndButton);
        PlayerStats.Instance.CurrentStamina = PlayerStats.Instance.MaxStamina;
        
        Instantiate(Player[PlayerStats.Instance.Select], new Vector2(-18.22f, -5.2f), Quaternion.identity);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(PlayerStats.Instance.Level == 3)
        {
            Instantiate(BossMonster[PlayerStats.Instance.Level - 1], new Vector2(-7f, 1f), Quaternion.identity);
            Lv3Platform.SetActive(true);
        }
        else
        {
            Lv3Platform.SetActive(false);
            Instantiate(BossMonster[PlayerStats.Instance.Level - 1], new Vector2(6.2f, -2.85f), Quaternion.identity);     
        }
        _currentHp = BossData.BossStats[PlayerStats.Instance.Level - 1].MaxHp;
        _maxHp = BossData.BossStats[PlayerStats.Instance.Level - 1].MaxHp;
    }

    void Update()
    {
        //게임 클리어 로직
        if(GameClearPanal.activeSelf && ClearImage.color.a < 1)
        {
            ClearImage.color += new Color(0,0,0,0.7f * Time.deltaTime);
        }
        if(ClearImage.color.a >= 1)
        {
            TotalDamageText.color += new Color(0, 0, 0, 0.7f * Time.deltaTime);
        }
        if(TotalDamageText.color.a >= 1)
        {
            TotalKillText.color += new Color(0, 0, 0, 0.7f * Time.deltaTime);
        }
        //이동 제한 로직
        if (player.transform.position.x < -20)
        {
            player.transform.position = new Vector2(-20, player.transform.position.y);
        }
        if (player.transform.position.x > 6.2f)
        {
            player.transform.position = new Vector2(6.2f, player.transform.position.y);
        }
       BossCurrentHpImage.fillAmount = _currentHp / _maxHp;
        if(PlayerStats.Instance.Level == 3 && BossMonsterCount <=0)
        {
            Invoke("GameClear", 3f);
        }
        else if (BossMonsterCount <= 0)
        {
            Invoke("Clear", 3f);
        }
    }
    void Clear()
    {
        BossMonsterHpBar.gameObject.SetActive(false);
        ClearPanal.SetActive(true);
    }
    void GameClear()
    {
        BossMonsterHpBar.gameObject.SetActive(false);
        GameClearPanal.SetActive(true);
        TotalDamageText.text = $"TotalDamage : {PlayerStats.Instance.TotalDamage}";
        TotalKillText.text = $"TotalKill : {PlayerStats.Instance.TotalKills}";
    }


    void OnClickMaxHpUpgrade()
    {
        PlayerStats.Instance.CurrentHp += 999;
        MaxHpUpgrade.gameObject.SetActive(false);
    }
    void OnClickAttackPowerUpgrade()
    {
        PlayerStats.Instance.BonusAttackPower += 20;
        AttackPowerUpgrade.gameObject.SetActive(false);
    }
    void OnClickNextButton()
    {
        PlayerStats.Instance.Level++;
        SceneManager.LoadScene("Map");
    }
    void OnClickEndButton()
    {
        SceneManager.LoadScene("Main");
    }
}
