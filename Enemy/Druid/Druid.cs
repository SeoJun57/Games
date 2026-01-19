using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Druid : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private int _currentMoveScale = 1;

    public Image CurrentHpImage;
    public GameObject Gold;
    public GameObject AttackHitBox;
    public LayerMask GroundLayer;
    public Material HitMaterial;

    private float _currentHp = 120 * PlayerStats.Instance.Level / 2;
    private float _maxHp = 120 * PlayerStats.Instance.Level / 2;

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
        else if (isCanMove)
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
        _animator.SetBool("isWalk", true);
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
            _animator.SetBool("isWalk", true);
        }
        // 움직이는 중이 아니라면 애니메이션 비활성화
        else
        {
            _animator.SetBool("isWalk", false);
        }
    }
    //공격로직
    IEnumerator CoAttack()
    {
        int scale = PlayerStats.Instance.PlayerTransform.x > transform.position.x ? 1 : -1;

        float distance = Vector3.Distance(PlayerStats.Instance.PlayerTransform, transform.position);
        if (distance < 2 && isAttack)
        {
            transform.localScale = new Vector3(scale, 1, 1);
            yield return new WaitForSeconds(0.1f);
            _animator.SetTrigger("Attack");
            isAttack = false;
            isCanMove = false;
        }
        yield return new WaitForSeconds(1.2f);

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
                StartCoroutine(CoDamage());
                isDamage = false;
            }
        }
        if (collision.CompareTag("Skill"))
        {
            if (isDamage)
            {
                _currentHp -= PlayerStats.Instance.SkillDamage;
                PlayerStats.Instance.TotalDamage += PlayerStats.Instance.SkillDamage;
                CurrentHpImage.fillAmount = _currentHp / _maxHp;
                StartCoroutine(CoDamage());
                isDamage = false;
            }
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
        yield return new WaitForSeconds(0.1f);
        isDamage = true;
    }
    public void MoveCan() { isCanMove = true; }
    public void Death() {
        isCanMove = false;
        PlayerStats.Instance.TotalKills++;
        Destroy(gameObject);
    }
}
