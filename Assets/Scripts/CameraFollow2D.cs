using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;         // 따라갈 목표 (플레이어)
    public Vector2 offset = new Vector2(0f, 1.2f); // 플레이어 대비 카메라 오프셋
    public float smoothTime = 0.15f; // 부드러움 (작을수록 타이트)
    public Vector2 clampX = new Vector2(float.NegativeInfinity, float.PositiveInfinity); // 이동 제한 (옵션)
    public Vector2 clampY = new Vector2(float.NegativeInfinity, float.PositiveInfinity);

    Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        // 스무스
        Vector3 smoothed = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothTime);

        // 클램프 적용 (있다면)
        float clampedX = Mathf.Clamp(smoothed.x, clampX.x, clampX.y);
        float clampedY = Mathf.Clamp(smoothed.y, clampY.x, clampY.y);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    // 유니티 에디터에서 target 없으면 자동으로 찾아보기 (편의용)
    [System.Obsolete]
    void Reset()
    {
        if (target == null)
        {
            var p = FindObjectOfType<PlayerController>();
            if (p != null) target = p.transform;
        }
    }
}
