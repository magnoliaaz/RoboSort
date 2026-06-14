using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Data Aturan Level")]
    public LevelData levelData;

    [Tooltip("Level1 = 1, Level2 = 2, Level3 = 3")]
    public int levelIndex;

    private const int MAX_LEVEL = 3;

    private void Start()
    {
        Debug.Log("Level Manager Siap! Menunggu Player menyelesaikan level...");

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        Debug.Log("Unlocked Level = " + unlockedLevel);
    }

    public int CalculateStar(int moveCount)
    {
        if (moveCount <= levelData.threeStarMoveLimit)
            return 3;

        if (moveCount <= levelData.twoStarMoveLimit)
            return 2;

        return 1;
    }

    public void CompleteLevel(int moveCount)
    {
        int star = CalculateStar(moveCount);

        Debug.Log("=== LEVEL SELESAI ===");
        Debug.Log("Nama Level: " + levelData.levelName);
        Debug.Log("Jumlah langkah: " + moveCount);
        Debug.Log("Bintang didapat: " + star);
        Debug.Log("=====================");

        SaveProgress(star);

        Invoke(nameof(LoadNextLevel), 2f);
    }

    private void SaveProgress(int star)
    {
        // Simpan bintang terbaik
        string starKey = "Level" + levelIndex + "_Star";

        int oldStar = PlayerPrefs.GetInt(starKey, 0);

        if (star > oldStar)
        {
            PlayerPrefs.SetInt(starKey, star);
        }

        // Unlock level berikutnya (maksimal Level 3)
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (levelIndex >= unlockedLevel && levelIndex < MAX_LEVEL)
        {
            PlayerPrefs.SetInt("UnlockedLevel", levelIndex + 1);
        }

        PlayerPrefs.Save();

        Debug.Log("Progress berhasil disimpan!");
    }

    public void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        else
        {
            Debug.Log("Selamat! Semua level telah selesai.");
        }
    }

    public void RestartLevel()
    {
        Debug.Log("Restart Level dipanggil!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Opsional untuk testing
    [ContextMenu("Reset Save Data")]
    public void ResetSaveData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("Semua save data berhasil dihapus.");
    }
}