using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Data Aturan Level")]
    public LevelData levelData;

    [Tooltip("Level1 = 1, Level2 = 2, Level3 = 3")]
    public int levelIndex;

    [Header("Referensi UI Kemenangan")]
    public WinUI uiMenang;

    private const int MAX_LEVEL = 3;

    private void Start()
{
    int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

    if (levelIndex > unlockedLevel)
    {
        Debug.Log("Level masih terkunci!");

        SceneManager.LoadScene("LevelStage");
        return;
    }

    Debug.Log("Level Manager Ready");
}

    public int CalculateStar(int moveCount)
    {
        if (moveCount <= levelData.threeStarMoveLimit) return 3;
        if (moveCount <= levelData.twoStarMoveLimit) return 2;
        return 1;
    }

    public void CompleteLevel(int moveCount)
    {
        int star = CalculateStar(moveCount);

        SaveProgress(star);

        WinUI winUI = FindFirstObjectByType<WinUI>(FindObjectsInactive.Include);

        if (winUI != null)
        {
            winUI.gameObject.SetActive(true);
            winUI.ShowWin(star);
        }
    }

    private void SaveProgress(int star)
    {
        string key = "Level" + levelIndex + "_Star";

        int oldStar = PlayerPrefs.GetInt(key, 0);

        PlayerPrefs.SetInt(key, star);

        // unlock next level
        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (levelIndex >= unlocked && levelIndex < MAX_LEVEL)
        {
            PlayerPrefs.SetInt("UnlockedLevel", levelIndex + 1);
        }

        PlayerPrefs.Save();
    }

    public void LoadNextLevel()
    {
        int current = SceneManager.GetActiveScene().buildIndex;

        if (current < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(current + 1);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}