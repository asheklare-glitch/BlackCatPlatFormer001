// ArrowTrap.cs
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject arrowPrefab; // 발사할 화살 프리팹
    public Transform firePoint;    // 화살이 발사될 위치
    public float fireRate = 2f;    // 발사 간격 (2초에 한 번)

    private float nextFireTime = 0f; // 다음 발사 시간

    // 트리거 범위 안에 무언가 들어와 '있는 동안' 계속 호출됨
    private void OnTriggerStay2D(Collider2D other)
    {
        // 들어온 것이 "Player" 태그를 가졌고, 발사할 시간이 되었다면
        if (other.CompareTag("Player") && Time.time >= nextFireTime)
        {
            Fire();
            // 다음 발사 시간 설정
            nextFireTime = Time.time + fireRate;
        }
    }

    void Fire()
    {
        // 화살 프리팹을 firePoint의 위치와 방향으로 생성(발사)
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
    }
}