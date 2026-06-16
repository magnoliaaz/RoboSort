using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [Header("Buttons")]
    public Button level2Button;
    public Button level3Button;

    [Header("Lock UI")]
    public GameObject lock2;
    public GameObject lock3;

    [Header("Stars UI Level 1")]
    public GameObject[] level1Stars;

    [Header("Stars UI Level 2")]
    public GameObject[] level2Stars;

    [Header("Stars UI Level 3")]
    public GameObject[] level3Stars;

    private void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // LOCK SYSTEM
        level2Button.interactable = unlockedLevel >= 2;
        level3Button.interactable = unlockedLevel >= 3;

        lock2.SetActive(unlockedLevel < 2);
        lock3.SetActive(unlockedLevel < 3);

        // LOAD STARS
        int star1 = PlayerPrefs.GetInt("Level1_Star", 0);
        int star2 = PlayerPrefs.GetInt("Level2_Star", 0);
        int star3 = PlayerPrefs.GetInt("Level3_Star", 0);

        SetStars(level1Stars, star1);
        SetStars(level2Stars, star2);
        SetStars(level3Stars, star3);
    }

    void SetStars(GameObject[] stars, int count)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(i < count);
        }
    }

    public void LoadLevel1() => SceneManager.LoadScene("Level1");
    public void LoadLevel2() => SceneManager.LoadScene("Level2");
    public void LoadLevel3() => SceneManager.LoadScene("Level3");
}