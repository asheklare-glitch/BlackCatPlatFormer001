using UnityEngine;


public class SceneTransition : MonoBehaviour
{
    [Tooltip("불러올 씬의 이름을 정확하게 입력하세요.")]
    public string sceneToLoad;

    // 다른 Collider가 트리거 안으로 들어왔을 때
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 들어온 것이 플레이어라면
        if (other.CompareTag("Player"))
        {
            SceneFader.instance.FadeToScene(sceneToLoad);
        }
    }
}