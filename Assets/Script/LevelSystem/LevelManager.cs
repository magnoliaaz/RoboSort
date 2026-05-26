using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData levelData;
    public int levelIndex;

    public int testMoveCount = 18;

    private void Start()
    {
        CompleteLevel(testMoveCount);
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

        Debug.Log("Level selesai!");
        Debug.Log("Nama level: " + levelData.levelName);
        Debug.Log("Jumlah langkah: " + moveCount);
        Debug.Log("Bintang yang didapat: " + star);
    }
}