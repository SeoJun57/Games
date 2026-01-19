using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cage : MonoBehaviour
{
    public GameObject GoldImage;
    public GameObject ButtonImage;

    void Update()
    {
        float dis = Vector3.Distance(PlayerStats.Instance.PlayerTransform, transform.position);
        if(dis < 2)
        {
            GoldImage.SetActive(true);
            ButtonImage.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F) && PlayerStats.Instance.Gold >= 90)
            {
                PlayerStats.Instance.Gold -= 90;
                Destroy(gameObject);
            }
        }
        else
        {
            GoldImage.SetActive(false);
            ButtonImage.SetActive(false);
        }



        
    }

}
