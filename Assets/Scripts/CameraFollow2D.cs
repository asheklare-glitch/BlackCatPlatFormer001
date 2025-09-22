using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;         // ���� ��ǥ (�÷��̾�)
    public Vector2 offset = new Vector2(0f, 1.2f); // �÷��̾� ��� ī�޶� ������
    public float smoothTime = 0.15f; // �ε巯�� (�������� Ÿ��Ʈ)
    public Vector2 clampX = new Vector2(float.NegativeInfinity, float.PositiveInfinity); // �̵� ���� (�ɼ�)
    public Vector2 clampY = new Vector2(float.NegativeInfinity, float.PositiveInfinity);

    Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        // ������
        Vector3 smoothed = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothTime);

        // Ŭ���� ���� (�ִٸ�)
        float clampedX = Mathf.Clamp(smoothed.x, clampX.x, clampX.y);
        float clampedY = Mathf.Clamp(smoothed.y, clampY.x, clampY.y);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    // ����Ƽ �����Ϳ��� target ������ �ڵ����� ã�ƺ��� (���ǿ�)
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
