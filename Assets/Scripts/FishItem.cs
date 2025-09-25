using UnityEngine;

public class FishItem : MonoBehaviour
{
    public GameObject collectionEffectPrefab; // 먹었을 때 나타날 파티클 이펙트 프리팹

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 부딪힌 것이 플레이어라면
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Heal(1); // 플레이어의 Heal 함수를 호출해서 체력을 채워줌
                PlayEffectAndDestroy();
            }
        }
    }

    private void PlayEffectAndDestroy()
    {
        // 파티클 이펙트가 설정되어 있다면, 현재 위치에 생성
        if (collectionEffectPrefab != null)
        {
            Instantiate(collectionEffectPrefab, transform.position, Quaternion.identity);
        }
        // 생선 오브젝트 자신을 파괴
        Destroy(gameObject);
    }
}