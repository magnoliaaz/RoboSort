using UnityEngine;

public class WinUI : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject[] stars;

    private void Start()
    {
        winPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShowWin(3);
        }
    }

    public void ShowWin(int starCount)
    {
        winPanel.SetActive(true);

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(i < starCount);
        }
    }
}