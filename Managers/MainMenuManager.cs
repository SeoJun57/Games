using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public AudioClip BGM;

    public Button StartButton;
    public Button ExitButton;
    //Vector3 mousePos = Input.mousePosition;
    void Start()
    {
        StartButton.onClick.AddListener(OnClickStartButton);
        ExitButton.onClick.AddListener(OnClickExitButton);
        if (PlayerStatsBar.Instance != null)
        {
            Destroy(PlayerStatsBar.Instance.gameObject);
        }
        if (MapManager.Instance != null)
        {
            Destroy(MapManager.Instance.gameObject);
        }
        if (PlayerStats.Instance != null)
        {
            Destroy(PlayerStats.Instance.gameObject);
        }
    }

    void OnClickStartButton()
    {
        SceneManager.LoadScene("Select");
    }
    void OnClickExitButton()
    {
       // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
