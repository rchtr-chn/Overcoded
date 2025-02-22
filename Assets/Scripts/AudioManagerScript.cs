using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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

    [Header("--------------- Audio slider ---------------")]
    [SerializeField] public AudioMixer audioMixer;

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



    public void setmusic(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
    }

    public void PlaySfx(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
