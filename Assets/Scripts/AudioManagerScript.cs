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
