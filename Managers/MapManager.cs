using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MapManager : MonoBehaviour
{
    #region Singleton
    // 클래스 내부에서만 접근 가능한 맵 인스턴스 생성
    private static MapManager s_Instance;
    //보호된 인스턴스 접근을 위해 프로퍼티 생성
    public static MapManager Instance
    {
        get
        {   // 맵 매니저 인스턴스가 null이면 return
            if (s_Instance == null) return null;
            // null이 아니라면 맵 매니저 인스턴스를 return
            else { return s_Instance; }
        }
    }
    #endregion
    private void Awake()
    {
        #region Singleton
        if (s_Instance == null)
        {   // 인스턴스가 null이라면 이 스크립트를 인스턴스화함
            s_Instance = this;
            // 싱글톤 오브젝트를 파괴 불가능하게함
            DontDestroyOnLoad(gameObject);
            // 맵 생성
            MapCreate();
        }
        else 
        {   // 인스턴스가 null이 아니라면 스크립트 삭제
            Destroy(gameObject);
        }
        #endregion
    }
    private int _currentLevel = PlayerStats.Instance.Level;
    private void Update()
    {
        if(_currentLevel != PlayerStats.Instance.Level)
        {
            MapCreate();
            _currentLevel = PlayerStats.Instance.Level;
            _currentMap = 0;
        }
    }

    private int[] _mapData = new int[18];
    private int _currentMap = 0;

    public int CurrentMap
    {
        get { return _currentMap; }
        set { _currentMap = value; }
    }
    public int[] MapData
    {
        get { return _mapData; }
    }
    /*
     * 0번 상자
     * 1번 몬스터
     * 2번 ?
     * 3번 상점
     * 4번 쉬는곳
     * 5번 레어몬스터
     */
    // 맵은 한 챕터에서는 변하면 안되기 때문에 싱글톤으로 구현해 저장
    void MapCreate()
    {
        // 첫 시작노드는 전투 고정
        MapData[0] = 1;
        // 마지막 노드는 보스 고정
        MapData[17] = 5;
        for (int i = 1; i < 17; i++)
        {   // 0부터 100사이의 난수 생성
            int ran = Random.Range(0, 101);
            if (ran < 50)
            {   // 수가 50보다 작으면 전투노드
                MapData[i] = 1;
            }
            else if (ran >= 50 && 60 > ran)
            {   // 수가 50이상 60미만이라면 상자노드
                MapData[i] = 0;
            }
            else if (ran >= 60 && 70 > ran)
            {   // 수가 60이상 70미만이라면 휴식노드
                MapData[i] = 4;
            }
            else if (ran >= 70 && 80 > ran)
            {   //수가 70이상 80미만이라면 상점 노드
                MapData[i] = 3;
            }
            else
            {   // 80이상이라면 랜덤노드
                MapData[i] = 2;
            }
        }
    }
}
