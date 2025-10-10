using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class CoffeeScript : MonoBehaviour
{
    public WaveScript WaveScript;

    [Header("Insanity Settings")]
    public float CurrentInsanity;
    public float DecreaseRate = 1f;
    public float MaxInsanity = 100f;
    public float LowInsanityThreshold = 50f;

    [Header("Drinking System")]
    public float HoldDuration = 3f; 
    public Image HoldCircle;
    public float HoldTimer = 0f;
    public bool IsHolding = false;

    [Header("Post Processing")]
    [SerializeField] private float _currentFocalLength;
    public float CurrentColorFilter;
    public float MaxColorFilter = 150f;
    public PostProcessProfile PostProcessProfile;
    private DepthOfField _depthOfField;
    private ColorGrading _colorGrading;

    void Start()
    {
        if(WaveScript == null) WaveScript = GameObject.Find("Player").GetComponent<WaveScript>();

        CurrentColorFilter = MaxColorFilter;
        CurrentInsanity = MaxInsanity;
        PostProcessProfile.TryGetSettings(out _depthOfField);
        PostProcessProfile.TryGetSettings(out _colorGrading);
        _colorGrading.colorFilter.value = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1f);
        _depthOfField.focalLength.value = 0f;
    }

    void Update()
    {
        if(WaveScript.CurrentWave < 5) return;

        if(WaveScript.CurrentWave == 6)
        {
            HoldDuration = 3f;
            MaxInsanity = 60f;
        }
        else if(WaveScript.CurrentWave == 8)
        {
            HoldDuration = 5f;
            MaxInsanity = 58f;
        }
        else if(WaveScript.CurrentWave == 9)
        {
            HoldDuration = 5f;
            MaxInsanity = 56f;
        }
        else if(WaveScript.CurrentWave == 10)
        {
            HoldDuration = 5f;
            MaxInsanity = 54f;
        }
        else if(WaveScript.CurrentWave > 10)
        {
            HoldDuration = 5f;
            MaxInsanity = 52f;
        }

        // Drinking System, Hold System
        if (IsHolding && CurrentInsanity <= LowInsanityThreshold) 
        {
            HoldTimer += Time.deltaTime;
            HoldCircle.fillAmount = HoldTimer / HoldDuration;
            if (HoldTimer >= HoldDuration)
            {
                CurrentInsanity = MaxInsanity;
                CurrentColorFilter = MaxColorFilter;
                HoldTimer = 0f;
                IsHolding = false;
            }
        }
        else
        {
            ResetHold();   
        }
        
        // Insanity System
        if (CurrentInsanity > 0)
        {
            CurrentInsanity -= DecreaseRate * Time.deltaTime;

            // Decreases Visibility as currentInsanity increases ( only Decreases when Blinking System is not active )
            _currentFocalLength = 100 - CurrentInsanity;
            CurrentColorFilter -= DecreaseRate * Time.deltaTime;
            CurrentColorFilter = Mathf.Max(CurrentColorFilter, 20f); 
            _colorGrading.colorFilter.value = new Color(CurrentColorFilter / 255f, CurrentColorFilter / 255f, CurrentColorFilter / 255f, 1f);
            _depthOfField.focalLength.value = _currentFocalLength;
        }

        // Permanent Blindness when Insanity reaches 0
        else
        {
            _colorGrading.colorFilter.value = new Color(20f / 255f, 20f / 255f, 20f / 255f, 1f); 
            _depthOfField.focalLength.value = 150f;
            IsHolding = false; 
        }
    }

    // Input System
    public void OnHold(InputAction.CallbackContext context)
    {
        if (CurrentInsanity > 0)
        {
            if (context.performed)
            {
                IsHolding = true;
            }
            else if (context.canceled)
            {
                IsHolding = false;
            }
        }
    }

    // Reset Hold System
    public void ResetHold()
    {
        IsHolding = false;
        HoldTimer = 0f;
        HoldCircle.fillAmount = 0f;
    }

    void OnDisable()
    {
        _colorGrading.colorFilter.value = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1f);
        _depthOfField.focalLength.value = 0f;
    }
}