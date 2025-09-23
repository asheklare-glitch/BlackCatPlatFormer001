using System.Collections;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    [Tooltip("플레이어가 밟고 나서 사라지기까지 걸리는 시간")]
    public float disappearDelay = 1f;

    [Tooltip("사라지고 나서 다시 나타나기까지 걸리는 시간")]
    public float reappearDelay = 3f;

    private SpriteRenderer sr;
    private BoxCollider2D col; // 또는 다른 Collider2D 타입
    private Vector3 initialPosition; // 초기 위치를 저장할 변수
    private bool isCrumbling = false; // 현재 부서지는 중인지 확인

    void Awake()
    {
        // 스크립트가 시작될 때 필요한 컴포넌트들을 미리 찾아둠
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        initialPosition = transform.position; // 초기 위치 저장
    }

    // 다른 Collider와 부딪혔을 때 호출됨
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 부딪힌 오브젝트의 태그가 "Player"이고, 현재 부서지는 중이 아니라면
        // 그리고 플레이어가 발판 위에서 밟았을 때만! (옆이나 아래에서 부딪힌 건 무시)
        if (collision.gameObject.CompareTag("Player") && !isCrumbling && IsPlayerOnTop(collision))
        {
            // 부서지는 코루틴 시작
            StartCoroutine(CrumbleAndReappear());
        }
    }

    // 플레이어가 위에서 밟았는지 확인하는 함수
    private bool IsPlayerOnTop(Collision2D collision)
    {
        // 충돌 지점들의 평균적인 수직 방향(normal)을 확인
        // 플레이어가 위에서 밟았다면, 충돌 방향은 거의 순수한 아래쪽(0, -1)에 가까움
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
        isCrumbling = true; // 부서지기 시작

        // 1. 정해진 시간만큼 기다림
        yield return new WaitForSeconds(disappearDelay);

        // 2. 발판을 비활성화 (사라지게 함)
        sr.enabled = false;
        col.enabled = false;

        // 3. 다시 나타날 때까지 기다림
        yield return new WaitForSeconds(reappearDelay);

        // 4. 발판을 다시 활성화 (원래 위치에 나타나게 함)
        transform.position = initialPosition; // 위치 초기화 (혹시 모르니)
        sr.enabled = true;
        col.enabled = true;

        isCrumbling = false; // 부서지는 과정 끝
    }
}
