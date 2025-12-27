using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public GameObject CurrentAttackCollider;
    public GameObject Effect;

    public AudioClip JumpSound;
    public AudioClip EffectSound;
    public LayerMask WallLayer;

    int attackCount = 0;
    float staminaCount = 0;

    bool isCanAttack = true;
    bool isGround = true;
    bool isCanMove = true;


    private Rigidbody2D _rb;
    private Animator _animator;
    private AudioSource _as;
    private List<GameObject> _platform = new List<GameObject>();

    private void Awake()
    {
        _as = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
        Attack();

        Jump();

        Defend();
        Run();
        StaminaCharge();
        Skill();
        if(PlayerStats.Instance.CurrentHp <= 0)
        {
            Dead();
        }
        Debug.DrawRay(transform.position, new Vector2(transform.localScale.x, 0), Color.red);
    }
    void Dead()
    {
        isCanMove = false;
        isCanAttack = false;
        _animator.SetTrigger("Dead");
    }
    
    void Move()
    {   // horizontal 버튼이 눌려지고 움직임 변수가 true라면 이동 로직 실행
        if (Input.GetButton("Horizontal") && isCanMove)
        {
            // horizontal의 값을 받아 좌우 구분
            float h = Input.GetAxis("Horizontal");
            // 좌우 구분에 따른 스케일 조정으로 방향 전환하기
            if (h != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = h > 0 ? 1f : -1f;
                transform.localScale = scale;
            }
            // 캐릭터가 움직일 방향
            Vector2 movement = new Vector2(h, 0);
            // 다른 요소와 상호작용을 위한 현재 플레이어의 위치 정보 념겨주기
            PlayerStats.Instance.PlayerTransform = transform.position;
            // Ray를 쏴서 앞에 벽이 있으면 움직임을 없애주기
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0), 0.8f, WallLayer);
            if (hit.collider != null) { movement = Vector2.zero; }
            // 벡터 정규화 해주기
            movement = movement.normalized;
            //플레이어의 속도에 따라 이동시키기
            transform.Translate(movement * PlayerStats.Instance.Speed * Time.deltaTime);
            //만약 땅 위라면 걷는 애니메이션 활성화
            if (isGround == true) { _animator.SetBool("isWalk", true); }
        }
        // 걷는 명령어를 입력받지 않았으면 애니메이션 비활성화
        else {  _animator.SetBool("isWalk", false);}
    }
    // 달리기 로직
    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isCanMove && Input.GetButton("Horizontal") && PlayerStats.Instance.CurrentStamina > 0 && PlayerStats.Instance.IsDefend == false && isGround)
        {
            float h = Input.GetAxis("Horizontal");

            staminaCount = 2;
            if (h != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = h > 0 ? 1f : -1f;
                transform.localScale = scale;
            }
            Vector2 movement = new Vector2(h, 0);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0), 0.8f, WallLayer);
            if (hit.collider != null)
            {
                movement = Vector2.zero;
            }
            movement = movement.normalized;
            _animator.SetBool("isRun", true);
            if (isGround == true)
            {
                PlayerStats.Instance.CurrentStamina -= 20 * Time.deltaTime;
                transform.Translate(movement * PlayerStats.Instance.RunSpeed * Time.deltaTime);
            }
            else
            {

                transform.Translate(movement * (PlayerStats.Instance.RunSpeed - 4) * Time.deltaTime);
            }

        }
        else
        {
            _animator.SetBool("isRun", false);
        }

    }

    void StaminaCharge()
    {
        if (staminaCount <= 0)
        {
            PlayerStats.Instance.CurrentStamina += 20 * Time.deltaTime;
            if (PlayerStats.Instance.CurrentStamina >= PlayerStats.Instance.MaxStamina)
            {
                PlayerStats.Instance.CurrentStamina = PlayerStats.Instance.MaxStamina;
            }
        }
        else
        {
            staminaCount -= 1 * Time.deltaTime;
            if ((staminaCount <= 0))
            {
                staminaCount = 0;
            }
        }
    }

    //공격로직
    void Attack()
    {

        if (Input.GetKeyDown(KeyCode.Z) && isGround && attackCount == 1 && isCanAttack)
        {
            _animator.SetTrigger("Attack2");
            CurrentAttackCollider.SetActive(true);
            StartCoroutine(CoAttackEnd());
            isCanMove = false;
            attackCount = 0;
            isCanAttack = false;
            // PlayerState.Instance.CurrentStamina -= 10;
            staminaCount = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Z) && isGround && attackCount == 0 && isCanAttack)
        {
            _animator.SetTrigger("Attack1");
            CurrentAttackCollider.SetActive(true);
            StartCoroutine(CoAttackEnd());
            isCanMove = false;
            attackCount++;
            isCanAttack = false;
            // PlayerState.Instance.CurrentStamina -= 10;
            staminaCount = 2;
        }
    }
    IEnumerator CoAttackEnd()
    {
        yield return new WaitForSeconds(0.25f);
        isCanMove = true;
        isCanAttack = true;
        CurrentAttackCollider.SetActive(false);
    }

    void Skill()
    {
        if (Input.GetKeyDown(KeyCode.C) && PlayerStats.Instance.CurrentStamina >= PlayerStats.Instance.SkillCost)
        {
            _as.PlayOneShot(EffectSound);
            PlayerStats.Instance.CurrentStamina -= PlayerStats.Instance.SkillCost;
            staminaCount = 2;
            _animator.SetTrigger("Attack3");
            StartCoroutine(CoSkillEnd());
            isCanMove = false;
            isCanAttack = false;
        }
    }
     IEnumerator CoSkillEnd()
    {
        yield return new WaitForSeconds(0.2f);
        isCanMove = true;
        isCanAttack = true;
    }
    //스킬 생성 로직 애니메이션에 사용
    public void UseSkill()
    {
        Instantiate(Effect, new Vector2(transform.position.x + 2 * transform.localScale.x, transform.position.y), Quaternion.identity);
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Space) && _platform != null && isGround)
        {          
            StartCoroutine(CoPlatform());
        }
        else if (Input.GetKeyDown(KeyCode.Space) && PlayerStats.Instance.JumpCount > 0 && PlayerStats.Instance.CurrentStamina > 0)
        {
            _as.PlayOneShot(JumpSound);
            _animator.SetTrigger("Jump");
            _rb.AddForce(Vector2.up * PlayerStats.Instance.JumpForce, ForceMode2D.Impulse);
            isGround = false;
            PlayerStats.Instance.JumpCount--;
            //PlayerStats.Instance.CurrentStamina -= 20;
            //staminaCount = 2;
            _rb.gravityScale = 2f;
            _animator.SetBool("isGround", false);
        }
    }
    public void SetGravity()
    {
        _rb.gravityScale = 4f;
    }
    IEnumerator CoPlatform()
    {
        for (int i = 0; i < _platform.Count; i++)
        {
            _platform[i].SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < _platform.Count; i++)
        {
            _platform[i].SetActive(true);

        }
        _platform.Clear();
    }
    void Defend()
    {
        if (Input.GetKey(KeyCode.X) && PlayerStats.Instance.CurrentStamina > 0)
        {
            _animator.SetBool("isDefend", true);
            staminaCount = 2;
            PlayerStats.Instance.CurrentStamina -= 30 * Time.deltaTime;
            PlayerStats.Instance.IsDefend = true;
            PlayerStats.Instance.Speed = 0.2f;
        }
        else
        {
            _animator.SetBool("isDefend", false);
            PlayerStats.Instance.IsDefend = false;
            PlayerStats.Instance.Speed = PlayerStats.Instance.MaxSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            PlayerStats.Instance.CurrentHp -= 2;
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _platform.Add(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && _rb.velocity.y <= 0)
        {

            PlayerStats.Instance.JumpCount = PlayerStats.Instance.MaxJump;
            isGround = true;
            _animator.SetBool("isGround", true);
        }
        else
        {
            isGround = false;
        }
        if (collision.gameObject.CompareTag("Platform") && _rb.velocity.y <= 0)
        {
            if (_rb.velocity.y <= 0)

                isGround = true;
            PlayerStats.Instance.JumpCount = PlayerStats.Instance.MaxJump;
            _animator.SetBool("isGround", true);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            _rb.velocity = Vector3.zero;
        }
    }
   
    public void GravityScaleDown()
    {
        _rb.gravityScale = 1;
    }

}
