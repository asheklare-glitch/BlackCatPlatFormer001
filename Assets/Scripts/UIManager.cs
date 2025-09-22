// UIManager.cs
using System.Collections; // Coroutine ����� ���� �߰�
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // --- �߰��� �κ� ���� ---
    public CanvasGroup gameOverCanvasGroup; // ���� ���� �г��� Canvas Group
    // --- �߰��� �κ� �� ---

    // --- �߰��� �κ� ���� ---
    // ���� ���� ȭ���� �ε巴�� �����ִ� �Լ�
    public void ShowGameOverScreen()
    {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        float fadeDuration = 1.0f; // 1�ʿ� ���� ��Ÿ������ ����
        float timer = 0f;

        // ������ ����(alpha=0)���� �������� ����(alpha=1)�� �� ������ �ݺ�
        while (timer < fadeDuration)
        {
            // ��� �ð��� ���� alpha ���� ������ ������Ŵ
            gameOverCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null; // �� ������ ���
        }

        gameOverCanvasGroup.alpha = 1; // �������� Ȯ���ϰ� 1�� ����

        // ���̵����� ������ ��ư�� ���� �� �ֵ��� ����
        gameOverCanvasGroup.interactable = true;
        gameOverCanvasGroup.blocksRaycasts = true;
    }
    // --- �߰��� �κ� �� ---

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Debug.Log("���� ���� ��ư Ŭ��!"); // �����Ϳ��� Ȯ���ϱ� ���� �α�
        Application.Quit(); // ���� ���� ���� (����� ���ӿ����� �۵�)
    }
}