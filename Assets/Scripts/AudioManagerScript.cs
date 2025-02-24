using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManagerScript : MonoBehaviour
{

    [Header("--------------- Audio Source ---------------")]
    public AudioSource musicSource;
    [SerializeField] AudioSource whiteNoise;
    [SerializeField] AudioSource SFXSource;

    [Header("--------------- Audio Clip ---------------")]
    public AudioClip backgroundNoise;
    public AudioClip mainBgm;
    public AudioClip mainBgmChaotic;
    public AudioClip death;
    public AudioClip gameOver;
    public AudioClip correct;
    public AudioClip incorrect;
    public AudioClip mumSfx;
    public AudioClip typing;
    public AudioClip flyHit;
    public AudioClip flyBuzz;
    public AudioClip popUpSfx;
    public AudioClip jump;
    public AudioClip drinkCoffee;
    public AudioClip openDoor;
    public AudioClip chatPopUp;
    public AudioClip yell;



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
        musicSource.clip = mainBgm;
        whiteNoise.clip = backgroundNoise;
        musicSource.loop = true;
        musicSource.Play();
        whiteNoise.Play();
    }
    public void PlaySfx(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void setBGM(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }
    public void setSFX(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

}
