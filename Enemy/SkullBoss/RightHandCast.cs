using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandCast : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {
       transform.Translate(new Vector3(-1, 0,0) * 7 *  Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerStats.Instance.CurrentHp -= 10;
        }
    }
}
