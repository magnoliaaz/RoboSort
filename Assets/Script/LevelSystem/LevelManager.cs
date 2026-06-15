using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Data Aturan Level")]
    public LevelData levelData;
    public int levelIndex;


    private void Start()
    {
        
        Debug.Log("Level Manager Siap! Menunggu Player menyelesaikan level...");
    }

    public int CalculateStar(int moveCount)
    {
        if (moveCount <= levelData.threeStarMoveLimit)
        {
            return 3;
        }
        else if (moveCount <= levelData.twoStarMoveLimit)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    public void CompleteLevel(int moveCount)
    {
        int star = CalculateStar(moveCount);

        Debug.Log("=== LEVEL SELESAI ===");
        Debug.Log("Nama Level: " + levelData.levelName);
        Debug.Log("Jumlah langkah aslimu: " + moveCount);
        Debug.Log("Bintang yang didapat: " + star + " Bintang ⭐️");
        Debug.Log("=====================");
    }
}