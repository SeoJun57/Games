using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperAttackHitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerStats.Instance.IsDefend == false)
        {
            PlayerStats.Instance.CurrentHp -= 50;
        }
    }
}
