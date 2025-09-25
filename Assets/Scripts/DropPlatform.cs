using System.Collections;
using UnityEngine;

public class DropPlatform : MonoBehaviour
{
    [Tooltip("�������� ���� ������ ���������� �ɸ��� �ð�")]
    public float disappearDelay = 2f;

    private Rigidbody2D rb;
    private bool hasBeenTriggered = false; // ����� �ߵ��Ǿ����� Ȯ��

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �̹� �ߵ��ߴٸ� �ƹ��͵� ���� ����
        if (hasBeenTriggered) return;

        // �ε��� ���� �÷��̾��̰�, ������ ����� ����
        if (collision.gameObject.CompareTag("Player") && IsPlayerOnTop(collision))
        {
            // �������� �ڷ�ƾ ����
            StartCoroutine(DropAndDisappear());
        }
    }

    // �÷��̾ ������ ��Ҵ��� Ȯ���ϴ� �Լ�
    private bool IsPlayerOnTop(Collision2D collision)
    {
        // �浹 ������ ����(normal)�� Ȯ��
        // �÷��̾ ������ ��Ҵٸ�, �浹 ������ �Ʒ���(-y)�� �����
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y < -0.5f)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator DropAndDisappear()
    {
        hasBeenTriggered = true;

        // rb.isKinematic = false; // �� ���� �Ʒ��� ���� ����
        rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(disappearDelay);
        Destroy(gameObject);
    }
}