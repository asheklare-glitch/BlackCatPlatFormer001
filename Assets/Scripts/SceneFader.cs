using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance; // 어떤 스크립트에서든 접근할 수 있는 단 하나의 인스턴스 (싱글톤)

    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private float fadeDuration = 1f;

    private void Awake()
    {
        // 싱글톤 패턴: 씬에 이미 인스턴스가 있다면 새로 생긴 건 파괴, 없다면 이걸 인스턴스로
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 이 오브젝트를 파괴하지 않음
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // 씬이 로드되었을 때 OnSceneLoaded 함수를 호출하도록 이벤트에 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 비활성화될 때 이벤트에서 등록 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 로드되면 자동으로 호출되는 함수
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 새 씬이 시작되면 페이드 인
        StartCoroutine(Fade(0f)); // 투명하게
    }

    // 다른 스크립트에서 이 함수를 호출하여 씬 전환을 시작
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(Fade(1f)); // 불투명하게 (Fade Out)
        SceneManager.LoadScene(sceneName);
    }

    // 알파 값을 조절하는 실제 페이드 코루틴
    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            // 경과 시간에 따라 알파 값을 시작 값에서 목표 값으로 변경
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            time += Time.deltaTime; // 시간 누적
            yield return null; // 한 프레임 대기
        }

        canvasGroup.alpha = targetAlpha; // 마지막엔 확실하게 목표 값으로 설정
    }
}