using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance; // � ��ũ��Ʈ������ ������ �� �ִ� �� �ϳ��� �ν��Ͻ� (�̱���)

    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private float fadeDuration = 1f;

    private void Awake()
    {
        // �̱��� ����: ���� �̹� �ν��Ͻ��� �ִٸ� ���� ���� �� �ı�, ���ٸ� �̰� �ν��Ͻ���
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� �� ������Ʈ�� �ı����� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // ���� �ε�Ǿ��� �� OnSceneLoaded �Լ��� ȣ���ϵ��� �̺�Ʈ�� ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // ��Ȱ��ȭ�� �� �̺�Ʈ���� ��� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ���� �ε�Ǹ� �ڵ����� ȣ��Ǵ� �Լ�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� ���� ���۵Ǹ� ���̵� ��
        StartCoroutine(Fade(0f)); // �����ϰ�
    }

    // �ٸ� ��ũ��Ʈ���� �� �Լ��� ȣ���Ͽ� �� ��ȯ�� ����
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(Fade(1f)); // �������ϰ� (Fade Out)
        SceneManager.LoadScene(sceneName);
    }

    // ���� ���� �����ϴ� ���� ���̵� �ڷ�ƾ
    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            // ��� �ð��� ���� ���� ���� ���� ������ ��ǥ ������ ����
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            time += Time.deltaTime; // �ð� ����
            yield return null; // �� ������ ���
        }

        canvasGroup.alpha = targetAlpha; // �������� Ȯ���ϰ� ��ǥ ������ ����
    }
}