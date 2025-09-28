using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverFadeScript : MonoBehaviour
{
    public Image uiImage;
    public float fadeDuration = 2f;

    private void Start()
    {
        if (uiImage == null)
            uiImage = GetComponent<Image>();

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);

        
        

        Color imageColor = uiImage.color;
        float startAlpha = imageColor.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0f, time);
            uiImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, newAlpha);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    
}
