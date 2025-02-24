using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBGMScript : MonoBehaviour
{
    public float fadeDuration = 6f, initialVolume;
    public AudioManagerScript audioScript;

    void Start()
    {
        if (audioScript == null)
            audioScript = GameObject.Find("Audio Manager").GetComponent<AudioManagerScript>();

        initialVolume = audioScript.musicSource.volume;

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);

        audioScript.musicSource.clip = audioScript.gameOver;
        audioScript.musicSource.loop = false;
        float startVolume = audioScript.musicSource.volume;
        audioScript.musicSource.Play();

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            audioScript.musicSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            yield return null;
        }

        audioScript.musicSource.volume = 0f;
        audioScript.musicSource.Stop();
    }
}
