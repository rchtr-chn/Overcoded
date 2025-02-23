using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverFadeScript : MonoBehaviour
{
    public Image uiImage;
    public float fadeDuration = 2f;
    public float fadeDuration2 = 6f;
    public AudioManagerScript audioScript;

    private void Start()
    {
        if (uiImage == null)
            uiImage = GetComponent<Image>();
        if(audioScript == null)
            audioScript = GameObject.Find("Audio Manager").GetComponent<AudioManagerScript>();

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);

        audioScript.musicSource.clip = audioScript.gameOver;
        audioScript.musicSource.loop = false;
        float startVolume = audioScript.musicSource.volume;
        audioScript.musicSource.Play();

        Color imageColor = uiImage.color;
        float startAlpha = imageColor.a;
        float time = 0f;

        while (time < fadeDuration2)
        {
            time += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0f, time);
            uiImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, newAlpha);
            audioScript.musicSource.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration2);
            yield return null;
        }

        uiImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0f);
        audioScript.musicSource.volume = 0f;
        audioScript.musicSource.Stop();
    }

    
}
