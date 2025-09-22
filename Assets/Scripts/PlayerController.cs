using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    // ... ���� �ڵ���� ��� ���� ...
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public int maxHealth = 3;
    public int currentHealth;
    public Image[] healthHearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public UIManager uiManager;
    public GameObject gameOverPanel;

    private bool isGrounded; // ���� ��Ҵ��� Ȯ���ϴ� ����
    public Transform groundCheck; // ���� Ȯ���� ��ġ (�÷��̾� �߹�)
    public float groundCheckDistance = 0.2f; // ���� Ȯ���ϴ� �������� ����
    public LayerMask groundLayer; // ������ �ν��� ���̾�

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool canDoubleJump = true;
    private bool isInvincible = false; // ���� ���� �������� Ȯ���ϴ� ����
    


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
                if (isGrounded)  // ���� ���� ��
                {
                    Jump();
                    canDoubleJump = true;
                }
                else if (canDoubleJump) // ���߿��� ���� ����
                {
                    Jump();
                    canDoubleJump = false;
                }
            }
    }

    private void CheckIfGrounded()
    {
        // groundCheck ��ġ���� �Ʒ� �������� groundCheckDistance ���̸�ŭ �������� ����
        // groundLayer�� �ش��ϴ� ��ü�� �����Ǹ� isGrounded�� true�� ��
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    }
    void Jump()
    {
        // ���� ���� �� �Ʒ��� �������� �ӵ��� ���ְ� �������� ������ �ް� ��
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // ���� ���ϴ� ������� ����
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

   
    // Trigger Collider�� �ٸ� Collider�� ������ �� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� Collider�� �±װ� "Fish" ���
        if (other.gameObject.CompareTag("Fish"))
        {
            Heal(1); // ü���� 1 ȸ��
            Destroy(other.gameObject); // �ε��� ���� ������Ʈ�� �ı�(����)
        }

        // ���� Collider�� �±װ� "DeathZone" �̶��
        else if (other.gameObject.CompareTag("DeathZone"))
        {
            currentHealth = 0; // ��� ü���� 0���� ����
            UpdateHealthUI();  // ü�� UI ������Ʈ
            Die();             // ��� ó�� �Լ� ȣ��
        }
    }

    // ü���� ȸ����Ű�� �Լ�
    public void Heal(int amount)
    {
        // ü���� amount��ŭ ���ϵ�, maxHealth�� ���� �ʵ��� �Ѵ�
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHealthUI(); // UI ������Ʈ
        Debug.Log("ü�� ȸ��! ���� ü��: " + currentHealth);
    }
    // --- �߰��� �κ� �� ---

    void TakeDamage(int damage)
    {
        // ���� ���¶�� �������� ���� �ʰ� �Լ��� ��� ����
        if (isInvincible) return;
        currentHealth -= damage;
        // ü���� 0 ������ �������� �ʵ��� �Ѵ�
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
        isInvincible = true; // ���� ���� ����

        float invincibilityDuration = 1.0f; // �� ���� �ð� (1.5��)
        float blinkInterval = 0.15f; // �����̴� ���� (0.15��)

        // ���� �ð� ���� �ݺ�
        for (float i = 0; i < invincibilityDuration; i += blinkInterval)
        {
            // �÷��̾��� Sprite Renderer�� ���� �״� �ؼ� �����̴� ȿ���� ��
            sr.enabled = !sr.enabled;
            // ������ ���ݸ�ŭ ���
            yield return new WaitForSeconds(blinkInterval);
        }

        sr.enabled = true; // �ڷ�ƾ�� ������ �ݵ�� ���̵��� ����
        isInvincible = false; // ���� ���� ����
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