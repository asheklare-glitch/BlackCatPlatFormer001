using UnityEngine;


public class SceneTransition : MonoBehaviour
{
    [Tooltip("�ҷ��� ���� �̸��� ��Ȯ�ϰ� �Է��ϼ���.")]
    public string sceneToLoad;

    // �ٸ� Collider�� Ʈ���� ������ ������ ��
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� ���� �÷��̾���
        if (other.CompareTag("Player"))
        {
            SceneFader.instance.FadeToScene(sceneToLoad);
        }
    }
}