using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sr;

    [Header("Sistem Penghitung Langkah")]
    public int jumlahLangkah = 0;
    public TextMeshProUGUI teksLangkahUI;

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
        {
            CobaGerak(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            CobaGerak(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CobaGerak(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            CobaGerak(Vector2.right);
        }
    }

    private void MainkanAnimasi(Vector2 arah)
    {
        if (animator == null || sr == null)
            return;

        // Kanan
        if (arah.x > 0)
        {
            sr.flipX = false;
            animator.Play("Walk_Side", 0, 0f);
        }
        // Kiri
        else if (arah.x < 0)
        {
            sr.flipX = true;
            animator.Play("Walk_Side", 0, 0f);
        }
        // Atas
        else if (arah.y > 0)
        {
            sr.flipX = false;
            animator.Play("Walk_Up", 0, 0f);
        }
        // Bawah
        else if (arah.y < 0)
        {
            sr.flipX = false;
            animator.Play("Walk_Down", 0, 0f);
        }
    }

    private void CobaGerak(Vector2 arah)
    {
        Vector2 targetPosisiPlayer = (Vector2)transform.position + arah;
        Collider2D objekDiDepan = Physics2D.OverlapCircle(targetPosisiPlayer, 0.2f);

        bool berhasilJalan = false;

        // Jalan biasa
        if (objekDiDepan == null || objekDiDepan.isTrigger)
        {
            transform.position = targetPosisiPlayer;
            berhasilJalan = true;
        }
        // Dorong box
        else if (objekDiDepan.CompareTag("Box"))
        {
            Vector2 targetPosisiBox = targetPosisiPlayer + arah;
            Collider2D objekDiBelakangBox = Physics2D.OverlapCircle(targetPosisiBox, 0.2f);

            if (objekDiBelakangBox == null || objekDiBelakangBox.isTrigger)
            {
                objekDiDepan.transform.position = targetPosisiBox;
                transform.position = targetPosisiPlayer;
                berhasilJalan = true;
            }
        }

        // Mainkan animasi hanya jika gerakan berhasil
        if (berhasilJalan)
        {
            MainkanAnimasi(arah);

            jumlahLangkah++;

            if (teksLangkahUI != null)
            {
                teksLangkahUI.text = "Langkah: " + jumlahLangkah;
            }

            LevelManager mesinKasir = FindObjectOfType<LevelManager>();

            if (mesinKasir != null)
            {
                int bintangSekarang = mesinKasir.CalculateStar(jumlahLangkah);

                Debug.Log(
                    "Langkah ke-" + jumlahLangkah +
                    " | Sisa Bintang: " + bintangSekarang
                );
            }
        }
    }
}