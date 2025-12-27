using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkKnightEffect : MonoBehaviour
{
    Transform Boss;
    float BossPos;
    private void Start()
    {
        Destroy(gameObject, 10);
        Boss = GameObject.FindGameObjectWithTag("Enemy").transform;
        BossPos = Boss.transform.localScale.x;
    }

    void Update()
    {
        if (BossPos == 1)
        {
            gameObject.transform.localScale = Vector3.one;
            transform.Translate(new Vector3(1,0) * 10 * Time.deltaTime);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1,1, 1);
            transform.Translate(new Vector3(-1,0) * 10 * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && PlayerStats.Instance.IsDefend == false)
        {
            PlayerStats.Instance.CurrentHp -= 10;
        }
        
    }
}
