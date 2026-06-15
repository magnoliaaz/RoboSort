using UnityEngine;

public class Trap : MonoBehaviour
{
    public LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Trigger level restart jika player atau box kena
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            Debug.Log(other.name + " kena spike! Restart level");
            levelManager.RestartLevel();
        }
    }
}