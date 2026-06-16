using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sr;

    [Header("Sistem Penghitung Langkah")]
    public int jumlahLangkah = 0;
    public TextMeshProUGUI teksLangkahUI;

    [Header("Pengaturan Grid")]
    [Tooltip("Ubah jadi 1 untuk Level 2 & 3, atau 1.43745 untuk Level 1 (lewat Inspector)")]
    public float jarakLangkah = 1f; 

    private void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (teksLangkahUI != null)
        {
            teksLangkahUI.text = "Langkah: " + jumlahLangkah;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            CobaGerak(Vector2.up);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            CobaGerak(Vector2.down);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            CobaGerak(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            CobaGerak(Vector2.right);
    }

    private void MainkanAnimasi(Vector2 arah)
    {
        if (animator == null || sr == null) return;

        if (arah.x > 0) { sr.flipX = false; animator.Play("Walk_Side", 0, 0f); }
        else if (arah.x < 0) { sr.flipX = true; animator.Play("Walk_Side", 0, 0f); }
        else if (arah.y > 0) { sr.flipX = false; animator.Play("Walk_Up", 0, 0f); }
        else if (arah.y < 0) { sr.flipX = false; animator.Play("Walk_Down", 0, 0f); }
    }

    private void CobaGerak(Vector2 arah)
    {
        
        Vector2 arahLangkah = arah * jarakLangkah;
        Vector2 targetPosisiPlayer = (Vector2)transform.position + arahLangkah;
        
        Collider2D[] objekDiDepan = Physics2D.OverlapCircleAll(targetPosisiPlayer, 0.2f);

        bool nabrakTembok = false;
        Collider2D boxDiDepan = null;

        foreach (Collider2D obj in objekDiDepan)
        {
            if (obj.gameObject == this.gameObject) continue; 
            if (obj.isTrigger) continue; 

            if (obj.CompareTag("Box"))
            {
                boxDiDepan = obj;
            }
            else
            {
                nabrakTembok = true; 
            }
        }

        if (nabrakTembok && boxDiDepan == null) return;

        bool berhasilJalan = false;

        if (boxDiDepan != null)
        {
            Vector2 targetPosisiBox = targetPosisiPlayer + arahLangkah;
            Collider2D[] objekDiBelakangBox = Physics2D.OverlapCircleAll(targetPosisiBox, 0.2f);
            
            bool boxBisaDidorong = true;
            foreach (Collider2D obj in objekDiBelakangBox)
            {
                if (obj.gameObject == boxDiDepan.gameObject) continue;
                if (!obj.isTrigger) 
                {
                    boxBisaDidorong = false;
                    break;
                }
            }

            if (boxBisaDidorong)
            {
                boxDiDepan.transform.position = targetPosisiBox;
                transform.position = targetPosisiPlayer;
                berhasilJalan = true;
            }
        }
        
        else if (!nabrakTembok)
        {
            transform.position = targetPosisiPlayer;
            berhasilJalan = true;
        }

        
        if (berhasilJalan)
        {
            MainkanAnimasi(arah);
            jumlahLangkah++;

            if (teksLangkahUI != null) teksLangkahUI.text = "Langkah: " + jumlahLangkah;

            LevelManager mesinKasir = FindFirstObjectByType<LevelManager>();
            if (mesinKasir != null)
            {
                
                mesinKasir.CalculateStar(jumlahLangkah);
            }
        }
    }
}