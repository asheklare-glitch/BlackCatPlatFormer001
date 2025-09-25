using UnityEngine;

public class ItemHover : MonoBehaviour
{
    [Tooltip("���Ʒ��� �����̴� �ӵ�")]
    public float hoverSpeed = 2f;

    [Tooltip("���Ʒ��� �����̴� ����")]
    public float hoverHeight = 0.1f;

    private Vector3 startPosition;

    void Start()
    {
        // �������� ���� ��ġ�� ����
        startPosition = transform.position;
    }

    void Update()
    {
        // sin �Լ��� �̿��� �ε巯�� ���� ������ ���
        // Time.time�� ���� -1�� 1 ���̸� ��� �պ��ϴ� sin �Լ��� Ư���� �̿�
        float newY = startPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

        // ���� y������ ��ġ�� ������Ʈ
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}