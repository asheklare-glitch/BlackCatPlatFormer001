// UIManager.cs
using System.Collections; // Coroutine 사용을 위해 추가
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // --- 추가된 부분 시작 ---
    public CanvasGroup gameOverCanvasGroup; // 게임 오버 패널의 Canvas Group
    // --- 추가된 부분 끝 ---

    // --- 추가된 부분 시작 ---
    // 게임 오버 화면을 부드럽게 보여주는 함수
    public void ShowGameOverScreen()
    {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        float fadeDuration = 1.0f; // 1초에 걸쳐 나타나도록 설정
        float timer = 0f;

        // 투명한 상태(alpha=0)에서 불투명한 상태(alpha=1)가 될 때까지 반복
        while (timer < fadeDuration)
        {
            // 경과 시간에 따라 alpha 값을 서서히 증가시킴
            gameOverCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null; // 한 프레임 대기
        }

        gameOverCanvasGroup.alpha = 1; // 마지막엔 확실하게 1로 설정

        // 페이드인이 끝나면 버튼을 누를 수 있도록 설정
        gameOverCanvasGroup.interactable = true;
        gameOverCanvasGroup.blocksRaycasts = true;
    }
    // --- 추가된 부분 끝 ---

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Debug.Log("게임 종료 버튼 클릭!"); // 에디터에서 확인하기 위한 로그
        Application.Quit(); // 실제 게임 종료 (빌드된 게임에서만 작동)
    }
}