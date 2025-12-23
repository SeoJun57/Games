using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestSceneManager : MonoBehaviour
{
    Transform player;

    public GameObject[] Player;
    public GameObject ButtonImage;
    public GameObject RestPanal;

    public Button RecoveryButton;
    public Button AttackPowerUpButton;
    bool isPanal = false;
    void Start()
    {
        RecoveryButton.onClick.AddListener(OnClickRecoveryButton);
        AttackPowerUpButton.onClick.AddListener(OnClickAttackPowerUpButton);
        Instantiate(Player[PlayerStats.Instance.Select], new Vector3(-8.22f, -5.2f), Quaternion.identity);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player.transform.position.x > -2.5f && player.transform.position.x < 2.5f)
        {
            ButtonImage.gameObject.SetActive(true);
            isPanal = true;
        }
        else
        {
            ButtonImage.gameObject.SetActive(false);
            isPanal = false;
        }
        OnRestPanal();
    }
    void OnRestPanal()
    {
        if (Input.GetKeyDown(KeyCode.F) && player.transform.position.x > -2.5f && player.transform.position.x < 2.5 && RestPanal.activeSelf == false && isPanal)
        {
            RestPanal.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F) && player.transform.position.x > -2.5f && player.transform.position.x < 2.5 && RestPanal.activeSelf || isPanal == false)
        {
            RestPanal.gameObject.SetActive(false);
        }
    }
    void OnClickRecoveryButton()
    {
        PlayerStats.Instance.CurrentHp += 20;
        RecoveryButton.gameObject.SetActive(false);
        AttackPowerUpButton.gameObject.SetActive(false);
    }
    void OnClickAttackPowerUpButton()
    {
        PlayerStats.Instance.BaseAttackPower += 5 ;
        AttackPowerUpButton.gameObject.SetActive(false);
        RecoveryButton.gameObject.SetActive(false );
    }
}
