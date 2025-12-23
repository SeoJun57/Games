using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    #region Singleton
    private static GameManager s_Instance = null;

    public static GameManager Instance
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

   
}
