using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstStageName = "Chapter1"; // ������ �������� �̸�

    public void StartGame()
    {
        // ���̵� ȿ���� �Բ� ù �������� �ε�
        if (SceneFader.instance != null)
        {
            SceneFader.instance.FadeToScene(firstStageName);
        }
        else // SceneFader�� ���� ��츦 ���
        {
            SceneManager.LoadScene(firstStageName);
        }
    }

    public void ExitGame()
    {
        Debug.Log("���� ����!");
        Application.Quit(); // ����� ���ӿ����� �۵�
    }
}