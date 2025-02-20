using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class CoffeeScript : MonoBehaviour
{
    [Header("Insanity Settings")]
    [SerializeField] private float currentInsanity;
    public float decreaseRate = 1f;
    public float maxInsanity = 100f;
    public float lowInsanityThreshold = 50f; 

    [Header("Drinking System")]
    public float holdDuration = 3f;   
    public Image holdCircle;
    public float holdTimer = 0f;
    public bool isHolding = false;

    [Header("Post Processing")]
    [SerializeField] private float currentFocalLength;
    public PostProcessProfile postProcessProfile;
    private DepthOfField depthOfField;
    private ColorGrading colorGrading;

    [Header("Blinking Effect")]
    public float minBlinkInterval = 10f;
    public float maxBlinkInterval = 15f;
    public float blinkDuration = 2f;
    private float blinkTimer;
    private Coroutine blinkCoroutine;

    void Start()
    {
        currentInsanity = maxInsanity;
        postProcessProfile.TryGetSettings(out depthOfField);
        postProcessProfile.TryGetSettings(out colorGrading);
        colorGrading.colorFilter.value = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1f);
        depthOfField.focalLength.value = 50f;
    }

    void Update()
    {
        // Drinking System, Hold System
        if (isHolding && currentInsanity <= lowInsanityThreshold) 
        {
            holdTimer += Time.deltaTime;
            holdCircle.fillAmount = holdTimer / holdDuration;
            if (holdTimer >= holdDuration)
            {
                currentInsanity = maxInsanity;
                holdTimer = 0f;
                isHolding = false;
            }
        }
        else
        {
            ResetHold();   
        }
        
        // Insanity System
        if (currentInsanity > 0)
        {
            currentInsanity -= decreaseRate * Time.deltaTime;

            // Decreases Visibility as currentInsanity increases ( only Decreases when Blinking System is not active )
            if (blinkCoroutine == null)
            {
                currentFocalLength = 150 - currentInsanity;
                depthOfField.focalLength.value = currentFocalLength;
            }

            // Blinking System only activates when Insanity is low
            if (currentInsanity <= lowInsanityThreshold)
            {
                blinkTimer -= Time.deltaTime;
                if (blinkTimer <= 0 && blinkCoroutine == null)
                {
                    blinkCoroutine = StartCoroutine(BlinkEffect());
                    blinkTimer = Random.Range(minBlinkInterval, maxBlinkInterval);
                }
            }
        }

        // Permanent Blindness when Insanity reaches 0
        else
        {
            depthOfField.focalLength.value = 150f;
            isHolding = false; 
            blinkCoroutine = null;
        }
    }

    // Input System
    public void OnHold(InputAction.CallbackContext context)
    {
        if (currentInsanity > 0)
        {
            if (context.performed)
            {
                isHolding = true;
            }
            else if (context.canceled)
            {
                isHolding = false;
            }
        }
    }

    // Reset Hold System
    public void ResetHold()
    {
        isHolding = false;
        holdTimer = 0f;
        holdCircle.fillAmount = 0f;
    }

    // Blinking Effect
    private IEnumerator BlinkEffect()
    {
        depthOfField.focalLength.value = 300f;
        colorGrading.colorFilter.value = new Color(20f / 255f, 20f / 255f, 20f / 255f, 1f);
        yield return new WaitForSeconds(blinkDuration);
        depthOfField.focalLength.value = currentFocalLength;
        colorGrading.colorFilter.value = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1f);
        yield return new WaitForSeconds(blinkDuration);
        blinkCoroutine = null;
    }
}