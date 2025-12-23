using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopSceneManager : MonoBehaviour
{
    Transform player;
    public GameObject[] Player;
    public GameObject ShopPanal;
    public GameObject ShopButtonImage;

    bool isPanal = false;
    void Start()
    { 
        Instantiate(Player[PlayerStats.Instance.Select], new Vector2(-8.22f, -5.2f), Quaternion.identity);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < -15)
        {
            player.transform.position = new Vector2(-15, player.transform.position.y);
        }
        if (player.transform.position.x > 10)
        {
            player.transform.position = new Vector2(10, player.transform.position.y);
        }

        if (player.transform.position.x > -7 && player.transform.position.x < -1.5f)
        {
            ShopButtonImage.gameObject.SetActive(true);
            isPanal = true;
        }
        else
        {
            ShopButtonImage.gameObject.SetActive(false);
            isPanal = false;
        }

        OnShopPanal();
    }
    void OnShopPanal()
    {
        
        if (Input.GetKeyDown(KeyCode.F) && player.transform.position.x > -7 && player.transform.position.x < -1.5f && ShopPanal.activeSelf == false && isPanal)
        {
            ShopPanal.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.F) && player.transform.position.x > -7 && player.transform.position.x < -1.5f && ShopPanal.activeSelf || isPanal == false)
        {
            ShopPanal.SetActive(false);
        }
    }

}
