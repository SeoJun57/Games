using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastHitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats.Instance.CurrentHp -= 5;
        }
    }
}
