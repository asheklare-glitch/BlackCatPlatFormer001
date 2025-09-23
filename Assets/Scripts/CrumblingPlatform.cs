using System.Collections;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    [Tooltip("�÷��̾ ��� ���� ���������� �ɸ��� �ð�")]
    public float disappearDelay = 1f;

    [Tooltip("������� ���� �ٽ� ��Ÿ������� �ɸ��� �ð�")]
    public float reappearDelay = 3f;

    private SpriteRenderer sr;
    private BoxCollider2D col; // �Ǵ� �ٸ� Collider2D Ÿ��
    private Vector3 initialPosition; // �ʱ� ��ġ�� ������ ����
    private bool isCrumbling = false; // ���� �μ����� ������ Ȯ��

    void Awake()
    {
        // ��ũ��Ʈ�� ���۵� �� �ʿ��� ������Ʈ���� �̸� ã�Ƶ�
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        initialPosition = transform.position; // �ʱ� ��ġ ����
    }

    // �ٸ� Collider�� �ε����� �� ȣ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ε��� ������Ʈ�� �±װ� "Player"�̰�, ���� �μ����� ���� �ƴ϶��
        // �׸��� �÷��̾ ���� ������ ����� ����! (���̳� �Ʒ����� �ε��� �� ����)
        if (collision.gameObject.CompareTag("Player") && !isCrumbling && IsPlayerOnTop(collision))
        {
            // �μ����� �ڷ�ƾ ����
            StartCoroutine(CrumbleAndReappear());
        }
    }

    // �÷��̾ ������ ��Ҵ��� Ȯ���ϴ� �Լ�
    private bool IsPlayerOnTop(Collision2D collision)
    {
        // �浹 �������� ������� ���� ����(normal)�� Ȯ��
        // �÷��̾ ������ ��Ҵٸ�, �浹 ������ ���� ������ �Ʒ���(0, -1)�� �����
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y < -0.5f)
            {
                return true;
            }
        }
        return false;
    }


    private IEnumerator CrumbleAndReappear()
    {
        isCrumbling = true; // �μ����� ����

        // 1. ������ �ð���ŭ ��ٸ�
        yield return new WaitForSeconds(disappearDelay);

        // 2. ������ ��Ȱ��ȭ (������� ��)
        sr.enabled = false;
        col.enabled = false;

        // 3. �ٽ� ��Ÿ�� ������ ��ٸ�
        yield return new WaitForSeconds(reappearDelay);

        // 4. ������ �ٽ� Ȱ��ȭ (���� ��ġ�� ��Ÿ���� ��)
        transform.position = initialPosition; // ��ġ �ʱ�ȭ (Ȥ�� �𸣴�)
        sr.enabled = true;
        col.enabled = true;

        isCrumbling = false; // �μ����� ���� ��
    }
}
