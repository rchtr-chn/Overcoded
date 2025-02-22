using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [Header("--------------- Audio Source ---------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource whiteNoise;
    [SerializeField] AudioSource SFXSource;

    [Header("--------------- Audio Clip ---------------")]
    public AudioClip backgroundNoise;
    public AudioClip bgm;
    public AudioClip death;

    public static AudioManagerScript instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);


    }
    void Start()
    {
        musicSource.clip = bgm;
        whiteNoise.clip = backgroundNoise;
        musicSource.Play();
        whiteNoise.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
