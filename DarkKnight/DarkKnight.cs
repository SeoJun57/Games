using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkKnight : MonoBehaviour
{
    Transform player;
    BossMonsterSceneManager bossMonsterSceneManager;
    private Animator _animator;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private AudioSource _as;

    public BossStatsSO BossData;
    private int _bossID = 1;

    public GameObject Attack1HitBox;
    public GameObject Attack2HitBox;

    public GameObject Attack2Effect;

    public AudioClip AttackSound;
    public AudioClip EffectSound;

    public Material HitMaterial;

    private float _currentHp = 200;
    private float _maxHp;

    bool isDamage = true;
    bool isCanMove = true;
    bool isAttack = true;
    void Start()
    {
        bossMonsterSceneManager = FindObjectOfType<BossMonsterSceneManager>();
        bossMonsterSceneManager.MaxHp = BossData.BossStats[_bossID].MaxHp;
        bossMonsterSceneManager.CurrentHp = BossData.BossStats[_bossID].MaxHp;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _as = GetComponent<AudioSource>();  
        StartCoroutine(CoAttack());
        _maxHp = BossData.BossStats[_bossID].MaxHp;
        _currentHp = BossData.BossStats[_bossID].MaxHp;
    }


    void Update()
    {
        if(_currentHp >0)
        {
            
            Move();
        }
        bossMonsterSceneManager.CurrentHp = _currentHp;
    }
    void Move()
    {
        int scale = player.transform.position.x > transform.position.x ? 1 : -1;
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (isCanMove && distance > 4)
        {
            transform.localScale = new Vector3(scale, 1, 1);
            transform.Translate(new Vector3(scale, 0, 0) * 2 * Time.deltaTime);
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }


    }
    //공격로직
    IEnumerator CoAttack()
    {
        int scale = player.transform.position.x > transform.position.x ? 1 : -1;

        float distance = Vector3.Distance(player.transform.position, transform.position);
        int ran = Random.Range(0, 11);
        if (distance <= 4 && isAttack)
        {
            if (ran > 2)
            {
                transform.localScale = new Vector3(scale, 1, 1);
                yield return new WaitForSeconds(0.1f);
                _animator.SetTrigger("Attack");
                isAttack = false;
                isCanMove = false;
            }
        }
        else if (isAttack)
        {
            transform.localScale = new Vector3(scale, 1, 1);
            yield return new WaitForSeconds(0.1f);
            _animator.SetTrigger("PowerAttack");
            isAttack = false;
            isCanMove = false;
        }

        yield return new WaitForSeconds(2);
        StartCoroutine(CoAttack());
    }
    //공격1 애니메이션에 사용
    public void HitBoxOn1()
    {
        Attack1HitBox.SetActive(true);
        StartCoroutine(CoHitEnd1());
    }
    public void PlaySound()
    {
        _as.PlayOneShot(AttackSound);
    }
    IEnumerator CoHitEnd1()
    {
        yield return new WaitForSeconds(0.1f);
        Attack1HitBox.SetActive(false);
    }
    public void HitBoxOn2()
    {
        Attack1HitBox.SetActive(true);
        StartCoroutine(CoHitEnd2());
    }
    IEnumerator CoHitEnd2()
    {
        yield return new WaitForSeconds(0.1f);
        Attack1HitBox.SetActive(false);
        yield return new WaitForSeconds(1);
        isCanMove = true;
        isAttack = true;
    }
    //공격2 애니메이션에 사용

    public void Attack2HitBoxOn()
    {
        Attack1HitBox.SetActive(true);
        StartCoroutine(CoHitEnd());
    }
    IEnumerator CoHitEnd()
    {
        yield return new WaitForSeconds(0.1f);
        Attack1HitBox.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        isCanMove = true;
        isAttack = true;
    }
    public void Effect()
    {
        if (gameObject.transform.localScale.x == 1)
        {
            Instantiate(Attack2Effect, new Vector2(transform.position.x + 2, transform.position.y - 1), Quaternion.identity);
        }
        else
        {
            Instantiate(Attack2Effect, new Vector2(transform.position.x - 2, transform.position.y - 1), Quaternion.identity);
        }
        _as.PlayOneShot(EffectSound);
    }



    //데미지 받는 로직
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            if (isDamage && _currentHp > 0)
            {
                bossMonsterSceneManager.CurrentHp = _currentHp;
                _currentHp -= PlayerStats.Instance.AttackPower;
                PlayerStats.Instance.TotalDamage += PlayerStats.Instance.AttackPower;
                isDamage = false;
                StartCoroutine(CoDamage());
            }
            if (_currentHp <= 0)
            {
                BossMonsterSceneManager.BossMonsterCount--;
                _animator.SetTrigger("Death");
            }
        }
        if (collision.CompareTag("Skill"))
        {
            if (isDamage && _currentHp > 0)
            {
                bossMonsterSceneManager.CurrentHp = _currentHp;
                _currentHp -= PlayerStats.Instance.SkillDamage;
                PlayerStats.Instance.TotalDamage += PlayerStats.Instance.SkillDamage;
                isDamage = false;
                 StartCoroutine(CoDamage());
            }
            if (_currentHp <= 0)
            {
                BossMonsterSceneManager.BossMonsterCount--;
                _animator.SetTrigger("Death");
            }
        }
    }
    IEnumerator CoDamage()
    {
        Material originalMatarial = _sr.material;
        _sr.material = HitMaterial;
        yield return new WaitForSeconds(0.2f);
        _sr.material = originalMatarial;
        isDamage = true;
    }
    public void MoveCan() { isCanMove = true; }

}
