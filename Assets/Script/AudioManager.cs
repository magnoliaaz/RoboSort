using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip goalSound;
    public AudioClip spikeSound;

    private AudioSource source;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        source = GetComponent<AudioSource>();
    }

    public void PlayGoal()
    {
        source.PlayOneShot(goalSound);
    }

    public void PlaySpike()
    {
        source.PlayOneShot(spikeSound);
    }
}