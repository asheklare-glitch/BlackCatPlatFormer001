// ArrowTrap.cs
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject arrowPrefab; // �߻��� ȭ�� ������
    public Transform firePoint;    // ȭ���� �߻�� ��ġ
    public float fireRate = 2f;    // �߻� ���� (2�ʿ� �� ��)

    private float nextFireTime = 0f; // ���� �߻� �ð�

    // Ʈ���� ���� �ȿ� ���� ���� '�ִ� ����' ��� ȣ���
    private void OnTriggerStay2D(Collider2D other)
    {
        // ���� ���� "Player" �±׸� ������, �߻��� �ð��� �Ǿ��ٸ�
        if (other.CompareTag("Player") && Time.time >= nextFireTime)
        {
            Fire();
            // ���� �߻� �ð� ����
            nextFireTime = Time.time + fireRate;
        }
    }

    void Fire()
    {
        // ȭ�� �������� firePoint�� ��ġ�� �������� ����(�߻�)
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
    }
}