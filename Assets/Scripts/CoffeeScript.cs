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
    public float currentColorFilter;
    public float maxColorFilter = 150f;
    public PostProcessProfile postProcessProfile;
    private DepthOfField depthOfField;
    private ColorGrading colorGrading;

    void Start()
    {
        currentColorFilter = maxColorFilter;
        currentInsanity = maxInsanity;
        postProcessProfile.TryGetSettings(out depthOfField);
        postProcessProfile.TryGetSettings(out colorGrading);
        colorGrading.colorFilter.value = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1f);
        depthOfField.focalLength.value = 0f;
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
                currentColorFilter = maxColorFilter;
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
            currentFocalLength = 100 - currentInsanity;
            currentColorFilter -= decreaseRate * Time.deltaTime;
            currentColorFilter = Mathf.Max(currentColorFilter, 20f); 
            colorGrading.colorFilter.value = new Color(currentColorFilter / 255f, currentColorFilter / 255f, currentColorFilter / 255f, 1f);
            depthOfField.focalLength.value = currentFocalLength;
        }

        // Permanent Blindness when Insanity reaches 0
        else
        {
            colorGrading.colorFilter.value = new Color(20f / 255f, 20f / 255f, 20f / 255f, 1f); 
            depthOfField.focalLength.value = 150f;
            isHolding = false; 
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

    void OnDisable()
    {
        colorGrading.colorFilter.value = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1f);
        depthOfField.focalLength.value = 0f;
    }
}