using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerScript : MonoBehaviour
{

    [Header("--------------- Audio Source ---------------")]
    public AudioSource MusicSource;
    [SerializeField] AudioSource WhiteNoise;
    [SerializeField] AudioSource SFXSource;

    [Header("--------------- Audio Clip ---------------")]
    public AudioClip BackgroundNoise;
    public AudioClip MainBgm;
    public AudioClip MainBgmChaotic;
    public AudioClip Death;
    public AudioClip GameOver;
    public AudioClip Correct;
    public AudioClip Incorrect;
    public AudioClip MumSfx;
    public AudioClip Typing;
    public AudioClip FlyHit;
    public AudioClip FlyBuzz;
    public AudioClip PopUpSfx;
    public AudioClip Jump;
    public AudioClip DrinkCoffee;
    public AudioClip OpenDoor;
    public AudioClip ChatPopUp;
    public AudioClip Yell;



    [Header("--------------- Audio slider ---------------")]
    [SerializeField] public AudioMixer AudioMixer;

    public static AudioManagerScript Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);


    }
    void Start()
    {
        MusicSource.clip = MainBgm;
        WhiteNoise.clip = BackgroundNoise;
        MusicSource.loop = true;
        MusicSource.Play();
        WhiteNoise.Play();
    }
    public void PlaySfx(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void setBGM(float volume)
    {
        AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }
    public void setSFX(float volume)
    {
        AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

}
