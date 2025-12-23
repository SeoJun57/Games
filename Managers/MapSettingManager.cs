using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MapSettingManager : MonoBehaviour
{
    // 씬 전환시 전환할 창 배열
    //public Button[] ChangeScene = new Button[6]; 
    /*
     * 0번 상자
     * 1번 몬스터
     * 2번 ?
     * 3번 상점
     * 4번 쉬는곳
     * 5번 레어몬스터
     */
    public Button[] MapButton = new Button[18];
    public Image[] MapImages = new Image[18];
    public Sprite[] Sprites = new Sprite[6];
    public Text LevelText;

    bool isCurrentMap = true;
    void Start()
    {
        MapSet();
        MapActivate();
        if (MapManager.Instance.CurrentMap - 1 >= 0)
        {
            MapButton[MapManager.Instance.CurrentMap - 1].interactable = true;
            MapButton[MapManager.Instance.CurrentMap - 1].onClick.RemoveAllListeners();
            
        }
        PlayerStats.Instance.CurrentStamina = PlayerStats.Instance.MaxStamina;
        LevelText.text = $"Level : {PlayerStats.Instance.Level}";
    }
    private void Update()
    {
        if (MapManager.Instance.CurrentMap - 1 >= 0)
        {
            if (isCurrentMap)
            {
                MapImages[MapManager.Instance.CurrentMap - 1].color -= new Color(0,0,0,0.001f);
                if (MapImages[MapManager.Instance.CurrentMap - 1].color == new Color(1, 1, 1, 0.3f))
                {
                    isCurrentMap = false;
                }
            }
            else
            {
                MapImages[MapManager.Instance.CurrentMap - 1].color += new Color(0,0,0,0.001f);
                if (MapImages[MapManager.Instance.CurrentMap - 1].color == new Color(1, 1, 1, 1))
                {
                    isCurrentMap = true;
                }
            }
        }
    }
    void MapSet()
    {   // 맵 매니저의 맵 데이터의 길이만큼 반복해 노드 전부에 기능 할당
        for (int i = 0; i < MapManager.Instance.MapData.Length; i++)
        {   // 싱글톤 패턴으로 저장되어 있는 데이터를 하나씩 가져와 값을 넣어줌
            switch (MapManager.Instance.MapData[i])
            {
                case 0:
                    MapButton[i].interactable = false;
                    MapButton[i].onClick.AddListener(OnClickChestButton);
                    MapImages[i].sprite = Sprites[MapManager.Instance.MapData[i]];
                    break;
                case 1:
                    MapButton[i].interactable = false;
                    MapButton[i].onClick.AddListener(OnClickMonsterButton);
                    MapImages[i].sprite = Sprites[MapManager.Instance.MapData[i]];
                    break;
                case 2:
                    MapButton[i].interactable = false;
                    //랜덤한 난수를 생성해 ?노드에 보스를 제외한 랜덤노드 부여
                    int ran = Random.Range(0, 4);
                    switch (ran)
                    {
                        case 0:
                            MapButton[i].onClick.AddListener(OnClickChestButton);
                            break;
                        case 1:
                            MapButton[i].onClick.AddListener(OnClickMonsterButton);
                            break;
                        case 2:
                            MapButton[i].onClick.AddListener(OnClickShopButton);
                            break;
                        case 3:
                            MapButton[i].onClick.AddListener(OnClickRestButton);
                            break;
                        default:
                            break;
                    }
                    MapImages[i].sprite = Sprites[MapManager.Instance.MapData[i]];
                    break;
                case 3:
                    MapButton[i].interactable = false;
                    MapButton[i].onClick.AddListener(OnClickShopButton);
                    MapImages[i].sprite = Sprites[MapManager.Instance.MapData[i]];
                    break;
                case 4:
                    MapButton[i].interactable = false;
                    MapButton[i].onClick.AddListener(OnClickRestButton);
                    MapImages[i].sprite = Sprites[MapManager.Instance.MapData[i]];
                    break;
                case 5:
                    MapButton[i].interactable = false;
                    MapButton[i].onClick.AddListener(OnClickRareMonsterButton);
                    MapImages[i].sprite = Sprites[MapManager.Instance.MapData[i]];
                    break;
                default:
                    break;
            }
        }
    }
    void MapActivate()
    {
        switch (MapManager.Instance.CurrentMap)
        {
            case 0:
                MapButton[0].interactable = true;
                MapImages[0].color = Color.white;
                break;
            case 1:
                MapButton[1].interactable = true;
                MapImages[1].color = Color.white;
                MapButton[2].interactable = true;
                MapImages[2].color = Color.white;
                break;
            case 2:
                MapButton[3].interactable = true;
                MapImages[3].color = Color.white;
                MapButton[4].interactable = true;
                MapImages[4].color = Color.white;
                break;
            case 3:
                MapButton[5].interactable = true;
                MapImages[5].color = Color.white;
                MapButton[6].interactable = true;
                MapImages[6].color = Color.white;
                break;
            case 4:
                MapButton[7].interactable = true;
                MapImages[7].color = Color.white;
                break;
            case 5:
                MapButton[7].interactable = true;
                MapImages[7].color = Color.white;
                MapButton[8].interactable = true;
                MapImages[8].color = Color.white;
                break;
            case 6:
                MapButton[8].interactable = true;
                MapImages[8].color = Color.white;
                MapButton[9].interactable = true;
                MapImages[9].color = Color.white;
                break;
            case 7:
                MapButton[9].interactable = true;
                MapImages[9].color = Color.white;
                break;
            case 8:
                MapButton[10].interactable = true;
                MapImages[10].color = Color.white;
                break;
            case 9:
                MapButton[10].interactable = true;
                MapImages[10].color = Color.white;
                MapButton[11].interactable = true;
                MapImages[11].color = Color.white;
                break;
            case 10:
                MapButton[11].interactable = true;
                MapImages[11].color = Color.white;
                break;
            case 11:
                MapButton[12].interactable = true;
                MapImages[12].color = Color.white;
                MapButton[13].interactable = true;
                MapImages[13].color = Color.white;
                break;
            case 12:
                MapButton[13].interactable = true;
                MapImages[13].color = Color.white;
                MapButton[14].interactable = true;
                MapImages[14].color = Color.white;
                break;
            case 13:
                MapButton[15].interactable = true;
                MapImages[15].color = Color.white;
                break;
            case 14:
                MapButton[15].interactable = true;
                MapImages[15].color = Color.white;
                MapButton[16].interactable = true;
                MapImages[16].color = Color.white;
                break;
            case 15:
                MapButton[16].interactable = true;
                MapImages[16].color = Color.white;
                break;
            case 16:
            case 17:
                MapButton[17].interactable = true;
                MapImages[17].color = Color.white;
                break;
            default:
                break;
        }
    }
    void OnClickMonsterButton()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        MapManager.Instance.CurrentMap = int.Parse(name.Replace("Button ", ""));
        SceneManager.LoadScene("MonsterScene");
    }
    void OnClickShopButton()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        MapManager.Instance.CurrentMap = int.Parse(name.Replace("Button ", ""));
        SceneManager.LoadScene("ShopScene");
    }
    void OnClickRestButton()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        MapManager.Instance.CurrentMap = int.Parse(name.Replace("Button ", ""));
        SceneManager.LoadScene("RestScene");
    }
    void OnClickChestButton()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        MapManager.Instance.CurrentMap = int.Parse(name.Replace("Button ", ""));
        SceneManager.LoadScene("ChestScene");
    }
    void OnClickRareMonsterButton()
    {
        SceneManager.LoadScene("BossMonster");
    }
}
