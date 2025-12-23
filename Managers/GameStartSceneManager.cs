using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartSceneManager : MonoBehaviour
{
    Transform player;
    public Button ShopButton;
    public Button ChestButton;
    public Button RestButton;
    public Button RareMonsterButton;
    public GameObject[] Player;

    private void Start()
    {

        Instantiate(Player[PlayerStats.Instance.Select], new Vector3(-5.9f, -3.3f), Quaternion.identity);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ShopButton.onClick.AddListener(OnClickShop);
        ChestButton.onClick.AddListener(OnClickChest);
        RestButton.onClick.AddListener(OnClickRest);    
        RareMonsterButton.onClick.AddListener(OnClickRareMonster);

    }
    private void Update()
    {
        if (player.transform.position.x < -9)
        {
            player.transform.position = new Vector2(-9, player.transform.position.y);
        }
        if (player.transform.position.x > 5)
        {
            player.transform.position = new Vector2(5, player.transform.position.y);
        }
    }
    void OnClickShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
    void OnClickChest()
    {
        SceneManager.LoadScene("ChestScene");
    }
    void OnClickRest()
    {
        SceneManager.LoadScene("RestScene");
    }
    void OnClickRareMonster()
    {
        SceneManager.LoadScene("BossMonster");
    }
}
