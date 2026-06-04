using UnityEngine;

public class GoalTarget : MonoBehaviour
{
    [Header("Masukkan UI Text You Win ke sini")]
    public GameObject teksMenang;

    private void OnTriggerEnter2D(Collider2D bendaYangMasuk)
    {
        if (bendaYangMasuk.CompareTag("Box"))
        {
            Debug.Log("Box masuk ke target!");

            AudioManager.Instance.PlayGoal();

            if (teksMenang != null)
            {
                teksMenang.SetActive(true);
            }
        }
    }
}