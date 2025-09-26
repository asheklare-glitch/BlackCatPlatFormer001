using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstStageName = "Chapter1"; // 시작할 스테이지 이름

    public void StartGame()
    {
        // 페이드 효과와 함께 첫 스테이지 로드
        if (SceneFader.instance != null)
        {
            SceneFader.instance.FadeToScene(firstStageName);
        }
        else // SceneFader가 없는 경우를 대비
        {
            SceneManager.LoadScene(firstStageName);
        }
    }

    public void ExitGame()
    {
        Debug.Log("게임 종료!");
        Application.Quit(); // 빌드된 게임에서만 작동
    }
}