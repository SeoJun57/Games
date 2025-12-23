using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MonsterSceneManager : MonoBehaviour
{

    Transform player;
    public GameObject[] Maps;

    public GameObject[] Player;
    public GameObject[] Monster;
    public GameObject ClearPanal;

    public Button NextButton;
    public Text RewardText;

    private int _maxEnemyCount = 1;
    static public int enemyCount = 1;
    private int _mapNum;
    private void Awake()
    {
        for (int i = 0; i < Maps.Length; i++)
        {
            Maps[i].SetActive(false);
        }
        int ran = Random.Range(0, Maps.Length);
        _mapNum = ran;
        Maps[_mapNum].SetActive(true);

    }
    void Start()
    {
        int ran1 = Random.Range(0, Monster.Length);
        switch (_mapNum)
        {
            case 0:
                Spwan(ran1, new Vector2(5, -2.6f));
                break;
            case 1:
                Spwan(ran1, new Vector2(-4, 7.6f));
                break;
            default:
                break;
        }
        PlayerStats.Instance.CurrentStamina = PlayerStats.Instance.MaxStamina;
        enemyCount = _maxEnemyCount;
        Time.timeScale = 1;
        
        NextButton.onClick.AddListener(OnClickNextButton);
        int randomGold = Random.Range(50, 100);
        RewardText.text = "+" + randomGold.ToString();
        //랜덤으로 몬스터 스폰해주기
        
        Instantiate(Player[PlayerStats.Instance.Select], new Vector2(-25f, -5.2f), Quaternion.identity);
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }


    void Update()
    {
        //if (enemyCount <= 0)
        //{
        //    Invoke("Clear", 3f);
        //}
    }
    //클리어시 실행
    void Clear()
    {
        ClearPanal.SetActive(true);
    }
    void OnClickNextButton()
    {
        PlayerStats.Instance.Gold += int.Parse(RewardText.text.Replace("+", ""));
        SceneManager.LoadScene("Map");
    }
    void Spwan(int ran, Vector2 pos)
    {
        Instantiate(Monster[ran], pos, Quaternion.identity);
    }
}
