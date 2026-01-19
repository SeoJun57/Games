using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public GameObject ButtonImage;

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(transform.position, PlayerStats.Instance.PlayerTransform);
        //f버튼 이미지 활성화,비활성화 로직
        if (dis < 2f)
        {
            ButtonImage.gameObject.SetActive(true);
        }
        else
        {
            ButtonImage.gameObject.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.F) && dis <= 2)
        {
            SceneManager.LoadScene("Map");
        }
    }
}
