using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Data Aturan Level")]
    public LevelData levelData;

    [Tooltip("Level1 = 1, Level2 = 2, Level3 = 3")]
    public int levelIndex;

    [Header("Referensi UI Kemenangan")]
    [Tooltip("Tarik objek Canvas Win UI buatan temanmu ke sini!")]
    public WinUI uiMenang; 

    private const int MAX_LEVEL = 3;

    private void Start()
    {
        Debug.Log("Level Manager Siap! Menunggu Player menyelesaikan level...");
    }

    public int CalculateStar(int moveCount)
    {
        if (moveCount <= levelData.threeStarMoveLimit) return 3;
        if (moveCount <= levelData.twoStarMoveLimit) return 2;
        return 1;
    }

    public void CompleteLevel(int moveCount)
    {
        int jumlahBintang = CalculateStar(moveCount);
        SaveProgress(jumlahBintang);

        Debug.Log("Menang! Langkah: " + moveCount + " | Bintang: " + jumlahBintang);

        
        WinUI uiMenang = FindFirstObjectByType<WinUI>(FindObjectsInactive.Include);
        
        if (uiMenang != null)
        {
            uiMenang.gameObject.SetActive(true); 

            uiMenang.ShowWin(jumlahBintang); 
        }
        else
        {
            Debug.LogError("Gawat, WinUI beneran nggak ada di Scene! Pastikan Canvas UI temanmu udah dimasukkan ke layar.");
        }
    }

    private void SaveProgress(int star)
    {
        string starKey = "Level" + levelIndex + "_Star";
        int oldStar = PlayerPrefs.GetInt(starKey, 0);

        if (star > oldStar) PlayerPrefs.SetInt(starKey, star);

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (levelIndex >= unlockedLevel && levelIndex < MAX_LEVEL)
        {
            PlayerPrefs.SetInt("UnlockedLevel", levelIndex + 1);
        }

        PlayerPrefs.Save();
    }

    public void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}