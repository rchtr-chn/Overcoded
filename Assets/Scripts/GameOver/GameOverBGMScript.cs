using System.Collections;
using UnityEngine;

public class GameOverBGMScript : MonoBehaviour
{
    public float FadeDuration = 6f;
    public float InitialVolume;
    public AudioManagerScript AudioScript;

    void Start()
    {
        if (AudioScript == null)
            AudioScript = GameObject.Find("Audio Manager").GetComponent<AudioManagerScript>();

        InitialVolume = AudioScript.MusicSource.volume;

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);

        AudioScript.MusicSource.clip = AudioScript.GameOver;
        AudioScript.MusicSource.loop = false;
        float startVolume = AudioScript.MusicSource.volume;
        AudioScript.MusicSource.Play();

        float time = 0f;
        while (time < FadeDuration)
        {
            time += Time.deltaTime;
            AudioScript.MusicSource.volume = Mathf.Lerp(startVolume, 0f, time / FadeDuration);
            yield return null;
        }

        AudioScript.MusicSource.volume = 0f;
        AudioScript.MusicSource.Stop();
    }
}
