using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeathAttackHitBox : MonoBehaviour
{
    private int _damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && PlayerStats.Instance.IsDefend == false)
        {
            PlayerStats.Instance.CurrentHp -= _damage;
        }
    }
}
