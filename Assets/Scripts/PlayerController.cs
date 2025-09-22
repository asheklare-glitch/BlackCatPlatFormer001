using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    // ... 이전 코드들은 모두 동일 ...
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public int maxHealth = 3;
    public int currentHealth;
    public Image[] healthHearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public UIManager uiManager;
    public GameObject gameOverPanel;

    private bool isGrounded; // 땅에 닿았는지 확인하는 변수
    public Transform groundCheck; // 땅을 확인할 위치 (플레이어 발밑)
    public float groundCheckDistance = 0.2f; // 땅을 확인하는 레이저의 길이
    public LayerMask groundLayer; // 땅으로 인식할 레이어

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool canDoubleJump = true;
    private bool isInvincible = false; // 현재 무적 상태인지 확인하는 변수
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        CheckIfGrounded();
        if (currentHealth <= 0) return;
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        if (move != 0)
            sr.flipX = move < 0;

        if (isGrounded)
        {
            if (move != 0)
                anim.Play("Run");
            else
                anim.Play("Idle");
        }

        if (Input.GetKeyDown(KeyCode.Space))
            
            {
                if (isGrounded)  // 땅에 있을 때
                {
                    Jump();
                    canDoubleJump = true;
                }
                else if (canDoubleJump) // 공중에서 더블 점프
                {
                    Jump();
                    canDoubleJump = false;
                }
            }
    }

    private void CheckIfGrounded()
    {
        // groundCheck 위치에서 아래 방향으로 groundCheckDistance 길이만큼 레이저를 쏴서
        // groundLayer에 해당하는 물체가 감지되면 isGrounded는 true가 됨
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    }
    void Jump()
    {
        // 더블 점프 시 아래로 떨어지는 속도를 없애고 점프력을 온전히 받게 함
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // 힘을 가하는 방식으로 변경
        anim.SetTrigger("JumpTrigger");
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(1);
        }
    }

   
    // Trigger Collider에 다른 Collider가 들어왔을 때 호출되는 함수
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 들어온 Collider의 태그가 "Fish" 라면
        if (other.gameObject.CompareTag("Fish"))
        {
            Heal(1); // 체력을 1 회복
            Destroy(other.gameObject); // 부딪힌 생선 오브젝트를 파괴(삭제)
        }

        // 들어온 Collider의 태그가 "DeathZone" 이라면
        else if (other.gameObject.CompareTag("DeathZone"))
        {
            currentHealth = 0; // 즉시 체력을 0으로 만듦
            UpdateHealthUI();  // 체력 UI 업데이트
            Die();             // 사망 처리 함수 호출
        }
    }

    // 체력을 회복시키는 함수
    public void Heal(int amount)
    {
        // 체력을 amount만큼 더하되, maxHealth를 넘지 않도록 한다
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHealthUI(); // UI 업데이트
        Debug.Log("체력 회복! 현재 체력: " + currentHealth);
    }
    // --- 추가된 부분 끝 ---

    void TakeDamage(int damage)
    {
        // 무적 상태라면 데미지를 입지 않고 함수를 즉시 종료
        if (isInvincible) return;
        currentHealth -= damage;
        // 체력이 0 밑으로 내려가지 않도록 한다
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        StartCoroutine(InvincibilityCoroutine());

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true; // 무적 상태 시작

        float invincibilityDuration = 1.0f; // 총 무적 시간 (1.5초)
        float blinkInterval = 0.15f; // 깜빡이는 간격 (0.15초)

        // 무적 시간 동안 반복
        for (float i = 0; i < invincibilityDuration; i += blinkInterval)
        {
            // 플레이어의 Sprite Renderer를 껐다 켰다 해서 깜빡이는 효과를 줌
            sr.enabled = !sr.enabled;
            // 정해진 간격만큼 대기
            yield return new WaitForSeconds(blinkInterval);
        }

        sr.enabled = true; // 코루틴이 끝나면 반드시 보이도록 설정
        isInvincible = false; // 무적 상태 종료
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < healthHearts.Length; i++)
        {
            if (i < currentHealth)
            {
                healthHearts[i].sprite = fullHeart;
            }
            else
            {
                healthHearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Die()
    {
        rb.linearVelocity = Vector2.zero;
        anim.Play("Dead");
        if (uiManager != null)
        {
            uiManager.ShowGameOverScreen();
        }
    }
}