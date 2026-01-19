using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class Skeleton : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    public LayerMask GroundLayer;

    public Material HitMaterial;

    public Image CurrentHpImage;
    public GameObject AttackHitBox;
    public GameObject Gold;


    private float _currentHp = 50 * PlayerStats.Instance.Level / 2;
    private float _maxHp = 50 * PlayerStats.Instance.Level / 2;

    private int _currentMoveScale = 1;



    bool isDamage = true;
    bool isCanMove = true;
    bool isAttack = true;
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CoAttack());
    }


    void Update()
    {   // Ray를 쏠 포지션 설정
        float rayPos = _currentMoveScale * -0.2f;
        // Ray를 쏴 바닥을 감지하는 변수 생성
        RaycastHit2D groundRay = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.4f), Vector3.down, 3f, GroundLayer);
        // 플레이어와의 거리를 측정해 변수에 저장
        float distance = Vector3.Distance(PlayerStats.Instance.PlayerTransform, transform.position);
        if (distance < 5 && isCanMove)
        {   // 플레이어와의 거리가 5미만이면서 움직임이 활성화 되었을 경우 추적 움직임 로직 실행
            TrackingMove(distance);
        }
        else if(isCanMove) 
        {   // 아닐경우 순찰(평범한)움직임 로직 실행
            CurrentMove(groundRay);
        }
        //죽었을때 로직
        if (_currentHp <= 0)
        {
            isCanMove = false;
            _animator.SetTrigger("Death");
        }
        // 체력바는 스케일이 바뀌어도 같은 방향으로 유지시켜주기 위해 scale 값 구하기
        int scale = transform.localScale.x == 1 ? 1 : -1;
        // 체력바를 항상 같은 방향으로 유지시켜줌
        CurrentHpImage.transform.localScale = new Vector3(scale, 1, 1);
    }
    void CurrentMove(RaycastHit2D ray)
    {
        if (ray.collider == null)
        {   // 받아온 값이 만약 null값이라면 낭떠러지이니 방향전환을 위해 스케일 바꿔주기
            _currentMoveScale = _currentMoveScale == 1 ? -1 : 1;
        }
        // 바꾼 스케일로 설정해주기
        transform.localScale = new Vector3(_currentMoveScale, 1, 1);
        // 변경된 스케일 방향으로 다시 이동
        transform.Translate(new Vector3(_currentMoveScale, 0, 0) * Time.deltaTime);
        // 걷는 애니메이션 활성화
        _animator.SetBool("isRun", true);
    }
    void TrackingMove(float dis)
    {   // 플레이어와 자신의 위치를 비교해 플레이어를 바라보는 방향으로 스케일값 지정
        int scale = PlayerStats.Instance.PlayerTransform.x > transform.position.x ? 1 : -1;
        // 받아온 값의 차이가 2보다 크다면 움직이게 하기
        if (dis > 2)
        {   // 미리 구한 스케일 값으로 방향 설정
            transform.localScale = new Vector3(scale, 1, 1);
            // 설정한 방향으로 이동시켜주기
            transform.Translate(new Vector3(scale, 0, 0) * Time.deltaTime);
            // 움직이는 중이라면 걷는 애니메이션 활성화
            _animator.SetBool("isRun", true);
        }
        // 움직이는 중이 아니라면 애니메이션 비활성화
        else
        {
            _animator.SetBool("isRun", false);
        }
    }
    //공격로직
    IEnumerator CoAttack()
    {   // 공격할때 설정할 스케일 값 받아오기
        int scale = PlayerStats.Instance.PlayerTransform.x > transform.position.x ? 1 : -1;
        //플레이어와 자신의 위치 차이를 구함
        float distance = Vector3.Distance(PlayerStats.Instance.PlayerTransform, transform.position);
        // 차이가 2보다 작거나 같으면 공격 실행
        if (distance <= 2 && isAttack)
        {   // 공격할 때는 이동을 막고 연속 공격이 안 되게 하기 위해 공격 가능 여부를 false로 만듬
            isAttack = false;
            isCanMove = false;
            // 공격할 때 플레이어 방향으로 스케일 변경
            transform.localScale = new Vector3(scale, 1, 1);    
            //공격 애니메이션 활성화
            _animator.SetTrigger("Attack");
        }
        // 연속 공격 방지를 위한 일정 시간 기다리기
        yield return new WaitForSeconds(1.2f);
        // 재귀 함수를 이용해 공격 로직이 끊기지 않게 해줌
        StartCoroutine(CoAttack());
    }
    public void HitBoxOn()
    {
        AttackHitBox.SetActive(true);
        StartCoroutine(CoHitEnd());
    }
    IEnumerator CoHitEnd()
    {
        yield return new WaitForSeconds(0.1f);
        AttackHitBox.SetActive(false);
        yield return new WaitForSeconds(2);
        isCanMove = true;
        isAttack = true;
    }
    //데미지 받는 로직
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            if (isDamage)
            {
                _currentHp -= PlayerStats.Instance.AttackPower;
                PlayerStats.Instance.TotalDamage += PlayerStats.Instance.AttackPower;
                CurrentHpImage.fillAmount = _currentHp / _maxHp;
                isDamage = false;
                StartCoroutine(CoDamage());
            }

        }
        if (collision.CompareTag("Skill"))
        {
            if (isDamage)
            {
                _currentHp -= PlayerStats.Instance.SkillDamage;
                PlayerStats.Instance.TotalDamage += PlayerStats.Instance.SkillDamage;
                CurrentHpImage.fillAmount = _currentHp / _maxHp;
                isDamage = false;
                StartCoroutine(CoDamage());
            }
        }
        if (_currentHp <= 0)
        {
            _animator.SetTrigger("Death");

        }
    }
    //애니메이션에 사용 : 죽으면 골드 떨어트리는 로직
    public void GoldCreat()
    {
        for (int i = 0; i < Random.Range(3, 8); i++)
        {
            Instantiate(Gold, transform.position, Quaternion.identity);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _currentMoveScale = _currentMoveScale == 1 ? -1 : 1;
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
    public void Death() { PlayerStats.Instance.TotalKills++; 
        Destroy(gameObject); }
}
