using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public GameObject HitBox;

    public void HitBoxOn()
    {
        HitBox.SetActive(true);
        StartCoroutine(CoHitEnd());
    }
    IEnumerator CoHitEnd()
    {
        yield return new WaitForSeconds(0.1f);
        HitBox.SetActive(false);
    }
    public void SpellDestroy()
    {
        Destroy(gameObject);
    }
    public void PlaySound()
    {
        BringerOfDeath.isCastSound = true;
    }
}
