using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject[] stars;

    private void Start()
    {
        winPanel.SetActive(false);
    }

    public void ShowWin(int starCount)
    {
        winPanel.SetActive(true);

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(i < starCount);
        }
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        int current = SceneManager.GetActiveScene().buildIndex;

        if (current < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(current + 1);
        }
    }

    public void BackToLevelStage()
    {
        SceneManager.LoadScene("LevelStage");
    }
}