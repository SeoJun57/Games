using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChestSceneManager : MonoBehaviour
{
    public GameObject[] Player;

    void Start()
    {    
        Instantiate(Player[PlayerStats.Instance.Select], new Vector3(-8.22f, -5.2f), Quaternion.identity);
    }

}
