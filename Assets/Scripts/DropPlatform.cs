using System.Collections;
using UnityEngine;

public class DropPlatform : MonoBehaviour
{
    [Tooltip("떨어지고 나서 완전히 사라지기까지 걸리는 시간")]
    public float disappearDelay = 2f;

    private Rigidbody2D rb;
    private bool hasBeenTriggered = false; // 기믹이 발동되었는지 확인

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 이미 발동했다면 아무것도 하지 않음
        if (hasBeenTriggered) return;

        // 부딪힌 것이 플레이어이고, 위에서 밟았을 때만
        if (collision.gameObject.CompareTag("Player") && IsPlayerOnTop(collision))
        {
            // 떨어지는 코루틴 시작
            StartCoroutine(DropAndDisappear());
        }
    }

    // 플레이어가 위에서 밟았는지 확인하는 함수
    private bool IsPlayerOnTop(Collision2D collision)
    {
        // 충돌 지점의 방향(normal)을 확인
        // 플레이어가 위에서 밟았다면, 충돌 방향은 아래쪽(-y)에 가까움
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

        // rb.isKinematic = false; // 이 줄을 아래와 같이 변경
        rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(disappearDelay);
        Destroy(gameObject);
    }
}