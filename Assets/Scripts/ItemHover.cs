using UnityEngine;

public class ItemHover : MonoBehaviour
{
    [Tooltip("위아래로 움직이는 속도")]
    public float hoverSpeed = 2f;

    [Tooltip("위아래로 움직이는 높이")]
    public float hoverHeight = 0.1f;

    private Vector3 startPosition;

    void Start()
    {
        // 아이템의 시작 위치를 저장
        startPosition = transform.position;
    }

    void Update()
    {
        // sin 함수를 이용해 부드러운 상하 움직임 계산
        // Time.time에 따라 -1과 1 사이를 계속 왕복하는 sin 함수의 특성을 이용
        float newY = startPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // 계산된 y값으로 위치를 업데이트
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}