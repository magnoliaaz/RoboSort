using UnityEngine;

public class GoalTarget : MonoBehaviour
{
    public bool isFilled = false; 

    private void OnTriggerEnter2D(Collider2D bendaYangMasuk)
    {
        if (bendaYangMasuk.CompareTag("Box"))
        {
            isFilled = true;
            Debug.Log("Satu Box masuk target!");
            AudioManager.Instance.PlayGoal();

            CekKemenangan(); 
        }
    }

    private void OnTriggerExit2D(Collider2D bendaYangKeluar)
    {
        if (bendaYangKeluar.CompareTag("Box"))
        {
            isFilled = false; 
        }
    }

    private void CekKemenangan()
    {
        GoalTarget[] semuaTarget = FindObjectsByType<GoalTarget>(FindObjectsSortMode.None);

        foreach (GoalTarget target in semuaTarget)
        {
            if (target.isFilled == false)
            {
                return; 
            }
        }

        Debug.Log("Semua Box Masuk! Menghitung bintang...");
        
        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
        LevelManager manager = FindFirstObjectByType<LevelManager>();

        if (player != null && manager != null)
        {
            manager.CompleteLevel(player.jumlahLangkah);
        }
    }
}