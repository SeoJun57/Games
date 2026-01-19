using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadPanal : MonoBehaviour
{
    public Image YouDiedImage;

    public Button ToMainButton;
    public Image ToMainButtonImage;
    void Start()
    {
        ToMainButton.onClick.AddListener(OnClickNextButton);
        ToMainButtonImage.color = new Color(1, 1, 1, 0);
       ToMainButton.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (YouDiedImage.color.a < 1)
        {
            YouDiedImage.color += new Color(0, 0, 0, 0.5f * Time.deltaTime);
        }
        else
        {
            if(ToMainButtonImage.color.a < 1)
            {
                ToMainButtonImage.color += new Color(0, 0, 0, 0.5f * Time.deltaTime);
            }
            else
                ToMainButton.enabled = true;
        }
    }
    void OnClickNextButton()
    {
        SceneManager.LoadScene("Main");
    }
}
