using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // �������ڸ��� ���������� ������ �ӵ��� ���ư�
        rb.linearVelocity = transform.right * speed;
    }

    // �ٸ� ������Ʈ�� �ε����� ��
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ȭ���� �ϴ� ��򰡿� �ε����� �ٷ� �����
        // (�÷��̾�� �������� �ִ� ó���� PlayerController���� �� ����)
        Destroy(gameObject);
    }
}

