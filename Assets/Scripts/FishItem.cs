using UnityEngine;

public class FishItem : MonoBehaviour
{
    public GameObject collectionEffectPrefab; // �Ծ��� �� ��Ÿ�� ��ƼŬ ����Ʈ ������

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �ε��� ���� �÷��̾���
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Heal(1); // �÷��̾��� Heal �Լ��� ȣ���ؼ� ü���� ä����
                PlayEffectAndDestroy();
            }
        }
    }

    private void PlayEffectAndDestroy()
    {
        // ��ƼŬ ����Ʈ�� �����Ǿ� �ִٸ�, ���� ��ġ�� ����
        if (collectionEffectPrefab != null)
        {
            Instantiate(collectionEffectPrefab, transform.position, Quaternion.identity);
        }
        // ���� ������Ʈ �ڽ��� �ı�
        Destroy(gameObject);
    }
}