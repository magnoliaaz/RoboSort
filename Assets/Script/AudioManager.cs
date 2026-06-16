using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sound Effects")]
    public AudioClip goalSound;
    public AudioClip spikeSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGoal()
    {
        audioSource.PlayOneShot(goalSound);
    }

    public void PlaySpike()
    {
        audioSource.PlayOneShot(spikeSound);
    }
}