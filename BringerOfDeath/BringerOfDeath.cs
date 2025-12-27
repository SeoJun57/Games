using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BringerOfDeath : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;
    private AudioSource _as;

    private int _bossID = 0;

    public GameObject AttackCollider;
    public GameObject Spell;

    public AudioClip AttackSound;
    public AudioClip CastSound;


    public Material HitMaterial;
    public BossStatsSO BossData;

    BossMonsterSceneManager bossMonsterSceneManager;
    float maxHp;
    float currentHp;

    public static bool isCastSound = false;
    bool isDonDu = true;

    bool isAttack = true;
    bool isCanMove = true;
    bool isCast = true;
    bool isDamage = true;
    void Start()
    {
        bossMonsterSceneManager = FindObjectOfType<BossMonsterSceneManager>();
        bossMonsterSceneManager.MaxHp = BossData.BossStats[_bossID].MaxHp;
        bossMonsterSceneManager.CurrentHp = BossData.BossStats[_bossID].MaxHp;
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _as = GetComponent<AudioSource>();  
        StartCoroutine(CoAttack());
        StartCoroutine(CoCast());
        //스크립터블 오브젝트에서 받아온 보스 체력정보 넣어주기
        maxHp = BossData.BossStats[_bossID].MaxHp;
        currentHp = BossData.BossStats[_bossID].MaxHp;
    }


    void Update()
    {
        if (isCastSound && isDonDu )
        {
            isCastSound = false;
            isDonDu = false;
            _as.PlayOneShot(CastSound);
        }
        BringerOfDeathMove();
        bossMonsterSceneManager.CurrentHp = currentHp;
    }
    //움직이는 로직
    void BringerOfDeathMove()
    {       
        float distance = Vector3.Distance(PlayerStats.Instance.PlayerTransform, transform.position);

        if (isCanMove && distance > 4f)
        {
            int scale = PlayerStats.Instance.PlayerTransform.x > transform.position.x ? -1 : 1;
            transform.localScale = new Vector3(scale, 1, 1);
            transform.Translate(new Vector3(-scale, 0, 0) * Time.deltaTime, Space.World);
            _animator.SetBool("isWalk", true);
        }
        else
        {
            _rb.velocity = Vector3.zero;
            _animator.SetBool("isWalk", false);
        }
    }
    //마법 사용 로직
    IEnumerator CoCast()
    {
        int ran = Random.Range(0, 101);
        if (isCast && ran > 70)
        {
            _animator.SetTrigger("Cast");
            isCast = false;
            isCanMove = false;
            isAttack = false;
        }
        yield return new WaitForSeconds(2f);
        isDonDu = true;
        isCast = true;
        isCanMove = true;
        isAttack = true;
        StartCoroutine(CoCast());
    }
    public void NewSpell()
    {
        Instantiate(Spell, new Vector3(PlayerStats.Instance.PlayerTransform.x, PlayerStats.Instance.PlayerTransform.y + 4), Quaternion.identity);
    }
    //공격로직
    IEnumerator CoAttack()
    {
        int scale = PlayerStats.Instance.PlayerTransform.x > transform.position.x ? -1 : 1;
        transform.localScale = new Vector3(scale, 1, 1);
        yield return new WaitForSeconds(0.2f);
        float distance = Vector3.Distance(PlayerStats.Instance.PlayerTransform, transform.position);
        if (distance <= 4f && isAttack)
        {
            _animator.SetTrigger("CurrentAttack");
            _as.PlayOneShot(AttackSound);
            isAttack = false;
            isCanMove = false;
            isCast = false;

        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(CoAttack());
    }
    public void SetAttack()
    {
        AttackCollider.SetActive(true);
        StartCoroutine(CoAttackEnd());
    }
    IEnumerator CoAttackEnd()
    {
        yield return new WaitForSeconds(0.1f);
        AttackCollider.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        isAttack = true;
        isCanMove = true;
        isCast = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            if (isDamage)
            {
                currentHp -= PlayerStats.Instance.AttackPower;
                bossMonsterSceneManager.CurrentHp = currentHp; 
                PlayerStats.Instance.TotalDamage += PlayerStats.Instance.AttackPower;
            StartCoroutine(CoDamage());
                isDamage = false;
            }
            if (currentHp <= 0)
            {
                _animator.SetTrigger("Death");
            }
        }
        if (collision.CompareTag("Skill"))
        {
            if (isDamage)
            {
                currentHp -= PlayerStats.Instance.SkillDamage;
                bossMonsterSceneManager.CurrentHp = currentHp;
                PlayerStats.Instance.TotalDamage += PlayerStats.Instance.SkillDamage;
            StartCoroutine(CoDamage());
                isDamage = false;
            }
            if (currentHp <= 0)
            {
                _animator.SetTrigger("Death");
            }
        }
    }
    IEnumerator CoDamage()
    {
        Material originalMatarial = _sr.material;
        _sr.material = HitMaterial;
        yield return new WaitForSeconds(0.1f);
        _sr.material = originalMatarial;
        isDamage = true;
    }
    public void Death()
    {
        Destroy(gameObject);
        BossMonsterSceneManager.BossMonsterCount--;
    }
}
