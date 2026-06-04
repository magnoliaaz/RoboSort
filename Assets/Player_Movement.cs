using UnityEngine;
using TMPro; 

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    [Header("Sistem Penghitung Langkah")]
    public int jumlahLangkah = 0;             
    public TextMeshProUGUI teksLangkahUI;     
    private void Start()
    {
        animator = GetComponent<Animator>();
        
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

    private void CobaGerak(Vector2 arah)
    {
        if (animator != null)
        {
            animator.SetFloat("moveX", arah.x);
            animator.SetFloat("moveY", arah.y);

            if (arah.x != 0) 
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x) * (arah.x > 0 ? 1 : -1); 
                transform.localScale = scale;
            }
        }
        
        Vector2 targetPosisiPlayer = (Vector2)transform.position + arah;
        Collider2D objekDiDepan = Physics2D.OverlapCircle(targetPosisiPlayer, 0.2f);

        bool berhasilJalan = false; 

        if (objekDiDepan == null || objekDiDepan.isTrigger)
        {
            transform.position = targetPosisiPlayer;
            berhasilJalan = true;
        }
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

       
        if (berhasilJalan == true)
        {
            jumlahLangkah++; 
            
            if (teksLangkahUI != null)
            {
                teksLangkahUI.text = "Langkah: " + jumlahLangkah;
            }

            
            LevelManager mesinKasir = FindObjectOfType<LevelManager>();
            
            if (mesinKasir != null)
            {
                int bintangSekarang = mesinKasir.CalculateStar(jumlahLangkah);
                
                Debug.Log("Langkah ke-" + jumlahLangkah + " | Sisa Bintang: " + bintangSekarang);
            }
            
        }
    }
}