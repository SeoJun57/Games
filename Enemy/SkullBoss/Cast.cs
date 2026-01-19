using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : MonoBehaviour
{
    Transform CastPos;
    public GameObject LeftHand;
    bool isDamage = true;
    private void Start()
    {
     
        CastPos = GameObject.FindGameObjectWithTag("CastPos").transform;
        transform.localScale = new Vector3( -1, 1, 1);
        Destroy(gameObject,3);
    }
    private void Update()
    {
        if (CastPos != null)
        {
            transform.position = CastPos.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isDamage)
        {
            PlayerStats.Instance.CurrentHp -= 2;
            StartCoroutine(CoDamage());
        }
    }
    IEnumerator CoDamage()
    {
        isDamage = false;
        yield return new WaitForSeconds(0.1f);
        isDamage = true;
    }
}
