using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverFadeScript : MonoBehaviour
{
    public Image UIImage;
    public float FadeDuration = 2f;

    private void Start()
    {
        if (UIImage == null)
            UIImage = GetComponent<Image>();

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);

        
        

        Color imageColor = UIImage.color;
        float startAlpha = imageColor.a;
        float time = 0f;

        while (time < FadeDuration)
        {
            time += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0f, time);
            UIImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, newAlpha);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    
}
