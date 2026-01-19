using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBoss : MonoBehaviour
{
    BossMonsterSceneManager bossMonsterSceneManager;
    public BossStatsSO BossData;
    public GameObject realBoss;
    public GameObject DeadEffect;

    public Material HitMaterial;

    private SpriteRenderer _sr;
    private Collider2D _c2;
    private int _bossID = 2;
    float currentHp;
    float maxHp;
    bool isDamage = true;
    private void Start()
    {
        _c2 = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
        bossMonsterSceneManager = FindObjectOfType<BossMonsterSceneManager>();
        maxHp = BossData.BossStats[_bossID].MaxHp;
        currentHp = BossData.BossStats[_bossID].MaxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            if (isDamage)
            {
                StartCoroutine(CoHit());
                currentHp -= PlayerStats.Instance.AttackPower;
                bossMonsterSceneManager.CurrentHp = currentHp;
                PlayerStats.Instance.TotalDamage += PlayerStats.Instance.AttackPower;
                isDamage = false;

            }
            StartCoroutine(CoDamage());
            if (currentHp <= 0)
            {
                StartCoroutine(CoDead());

            }

        }
        if (collision.CompareTag("Skill"))
        {
            if (isDamage)
            {
                currentHp -= PlayerStats.Instance.SkillDamage;
                PlayerStats.Instance.TotalDamage += PlayerStats.Instance.SkillDamage;
                isDamage = false;
            }
            StartCoroutine(CoDamage());
            if (currentHp <= 0)
            {
                Destroy(realBoss);
                BossMonsterSceneManager.BossMonsterCount -= 1;
            }
            bossMonsterSceneManager.CurrentHp = currentHp;
        }
    }
    IEnumerator CoHit()
    {
        Material originalMatarial = _sr.material;
        _sr.material = HitMaterial;
        yield return new WaitForSeconds(0.2f);
        _sr.material = originalMatarial;
    }
    IEnumerator CoDead()
    {
        _c2.enabled = false;
        for (float i = 0; i <= 45; i++)
        {
            transform.Rotate(new Vector3(0, 0, i * Time.deltaTime));
        }
        Instantiate(DeadEffect, transform.position, Quaternion.identity);
        CameraMove.GameClearMove = true;
        yield return new WaitForSeconds(0.4f);
        while (_sr.color.a >= 0)
        {
            _sr.color -= new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.4f);
        Destroy(realBoss);
        BossMonsterSceneManager.BossMonsterCount -= 1;
    }
    IEnumerator CoDamage()
    {
        yield return new WaitForSeconds(0.1f);
        isDamage = true;
    }

}
