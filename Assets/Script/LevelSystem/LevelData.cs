using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Robosort/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName;

    public int threeStarMoveLimit;
    public int twoStarMoveLimit;
}