using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTypingScript : MonoBehaviour
{
    private readonly string[] wordBank =
    {
        "set player.speed = 5;", "set gravity = on;", "enable hitbox;",
        "disable collision;", "set obstacle.count = 10;", "reset timer;",
        "increase health by 20;", "set camera.zoom = 2;", "set wallJump = true;",
        "spawn prefab obstacle;", "set player.jumpHeight = 3;", "set obstacle.speed = 5;",
        "set physics.friction = 0.1;", "set.player.spriteRenderer = true;", "FixPhysics();",
        "UnstuckPlayer();", "PatchCollision();", "ReloadScene();",
        "DebugGameAI();", "RestoreJump();", "ResetWorldState();",
        "ForceRespawn();", "EnableAutoSave();", "ToggleStealthMode();",
        "EnableCheckDamage();", "RegenerateLevel();", "ClearMemoryLeaks();",
        "BoostFPS();", "FixLatency();", "EnableDebugCamera();",
        "FixPlayerBug();", "GetNextWave();", "SpawnGameObjects();",
        "ReduceLagSpikes();", "AdjustCameraShake();", "RemoveSoftLock();",
        "PatchTextureErrors();", "DebugLogs();", "FixAudioGlitches();",
        "Set timer = 0;", "SimulateBugReport();", "ApplyGravity();",
        "ResetCharacterMovement();", "FixCollision();", "SetObjectReferences();",
        "SetVelocity(0);", "AdjustFriction();", "ClampSpeed();",
        "RestoreMomentum();", "EnableGroundCheck();", "NormalizeMovement();",
        "FixShader();", "ReloadTextures();", "AdjustBrightness();",
        "ResetViewport();", "RestoreResolution();", "ClearArtifacts();",
        "StabilizeRendering();", "FixTransparency();", "RefreshUI();",
        "DisplayLogs();", "disableErrors();", "RestoreAudio();", 
        "UmnuteSounds();", "SyncAudio();", "ResetVolume();",
        "FixEcho();", "FixLocalizationError();", "AdjustPitch();",
        "ReloadMusic();", "DisableNoise();", "NormalizeBass();",
        "ClearDistortion();", "ResetControls();", "SetDefaultControls();",
        "EnableInput();", "SyncKeyboard();", "CalibrateMouse();",
        "FixControls();", "InvertAxis();", "SetSensitivity(1.0f);",
        "RestoreBindings();", "DisableStickyKeys();", "FixCamera();",
        "FixCameraAngle();", "ResetZoom();", "StabilizeCameraView();",
        "InvertCamera();", "SetViewAngle();", "EnableDepthBuffer();",
        "FixPerspective();", "AdjustFocus();", "CenterViewport();",
        "ReloadShaders();", "RestartGameAI", "FixReference();",
        "SyncBehavior();", "ResetObstacles();", "DisableRagdoll();",
        "RestoreSpeed();", "ReloadObjective();", "ResetAnimations();",
        "EnableCollisions();", "AdjustTimer();", "AdjustScore();",
        "OptimizeBandwidth()", "ReconnectServer();", "StabilizeConnections();",
        "FlushCache();", "EnableFirewall();", "DisableCheats();",
        "ResetState();", "SyncFrames();", "ClearCache();",
        "PatchErrors();", "EnableLogging();", "RestoreDefaults();",
        "RebuildProject();", "UpdateDrivers();", "FreeMemory();",
        "ClearLeaks();", "OptimizeMemoryUsage();", "PurgeCache();",
        "CompactData();", "FixOverload();", "StabilizeFrames();",
        "ResetHeap();", "DisableGarbage();", "EnableCompression();"
    };

    public Text PromptText;
    public Text InputText;
    public PlayerMovementScript PlayerMovementScript;
    public ObstacleSpawnerScript ObstacleSpawnerScript;
    public AudioManagerScript AudioScript;
    public float Timer, TimerCap;
    private bool _bugAfflicted = false;
    private int _bugIndex = -1;

    private string _currentPrompt = string.Empty;
    private string _currentInput = string.Empty;
    void Start()
    {
        //timerCap = 10f;
        //initialize coding minigame, CHANGE WHEN WORDBANK IS ADDED
        if (AudioScript == null) AudioScript = GameObject.Find("Audio Manager").GetComponent<AudioManagerScript>();
        ObstacleSpawnerScript = GameObject.Find("Obstacle-Spawner").GetComponent<ObstacleSpawnerScript>();
        GetNewPrompt(wordBank[Random.Range(0, wordBank.Count())]);
        InvisibleText();
    }

    void Update()
    {
        if(PlayerMovementScript.IsBugged)
        {
            //timer += Time.deltaTime;
            //if(timer > timerCap)
            //{
            //    timer = 0;
            //    audioScript.PlaySfx(audioScript.incorrect);
            //    obstacleSpawnerScript.prefabSpeed += 1.5f;

            //    currentInput = "";
            //    inputText.text = "";
            //    InvisibleText();
            //    playerMovementScript.isBugged = false;

            //    GetNewPrompt(wordBank[Random.Range(0, wordBank.Count())]);
            //    return;
            //}
            if(!_bugAfflicted)
            {
                AfflictDebuffs();
                _bugAfflicted = true;
            }

            VisibleText();
            TypeInput();
        }
    }

    private void AfflictDebuffs()
    {
        _bugIndex = Random.Range(0, 2);

        switch (_bugIndex)
        {
            case 0:
                Debug.Log("Afflicting debuff: Increase obstacle speed");
                // Example debuff: Increase obstacle speed
                ObstacleSpawnerScript.PrefabSpeed += 2f;
                break;
            case 1:
                Debug.Log("Afflicting debuff: Invert player controls");
                // Example debuff: invert player controls
                (PlayerMovementScript.JumpKey, PlayerMovementScript.DuckKey) =
                    (PlayerMovementScript.DuckKey, PlayerMovementScript.JumpKey);
                break;
            case 2:
                Debug.Log("Afflicting debuff: Decrease gravity");
                // Example debuff: Decrease gravity
                PlayerMovementScript.Rb.gravityScale -= 1f;
                break;
            default:
                // Default case if needed
                break;
        }
    }

    private void RevertDebuffs(int index)
    {
        switch(index)
        {
            case 0:
                // Revert obstacle speed increase
                ObstacleSpawnerScript.PrefabSpeed -= 2f;
                break;
            case 1:
                // Revert player controls
                (PlayerMovementScript.JumpKey, PlayerMovementScript.DuckKey) =
                    (PlayerMovementScript.DuckKey, PlayerMovementScript.JumpKey);
                break;
            case 2:
                // Revert gravity change
                PlayerMovementScript.Rb.gravityScale += 1f;
                break;
            default:
                // Default case if needed
                break;
        }
    }

    private void GetNewPrompt(string word)
    {
        _currentPrompt = word;
        PromptText.text = _currentPrompt;
    }

    private void TypeInput()
    {
        foreach (char c in Input.inputString)
        {
            AudioScript.PlaySfx(AudioScript.Typing);

            if (c == '\n' || c == '\r')
            {
                Timer = 0f;
                if(!CodeValidity())
                {
                    AudioScript.PlaySfx(AudioScript.Incorrect);
                    Debug.Log("bugIndex =" + _bugIndex);
                }
                else
                {
                    Debug.Log("bugIndex =" + _bugIndex);
                    if (_bugAfflicted || _bugIndex != -1)
                    {
                        Debug.Log("bug removed!");
                        RevertDebuffs(_bugIndex);
                        _bugAfflicted = false;
                    }

                    _bugIndex = -1;
                    AudioScript.PlaySfx(AudioScript.Correct);
                }


                _currentInput = "";
                InputText.text = "";
                InvisibleText();
                PlayerMovementScript.IsBugged = false;

                GetNewPrompt(wordBank[Random.Range(0, wordBank.Count())]);
                return;
            }
            else if (c == '\b')
            {
                if(_currentInput.Length > 0)
                {
                    _currentInput = _currentInput.Substring(0, _currentInput.Length - 1);
                    InputText.text = _currentInput;
                }
                return;
            }

            if (_currentInput.Length < _currentPrompt.Length)
            {
                _currentInput += c;
                InputText.text = _currentInput;
            }

        }
    }

    public bool CodeValidity()
    {
        if(_currentInput == _currentPrompt)
        {
            return true;
        }
        return false;
    }
    public void InvisibleText()
    {
        Color promptColor = PromptText.color;
        Color inputColor = InputText.color;
        promptColor.a = inputColor.a = 0f;
        PromptText.color = promptColor;
        InputText.color = inputColor;
    }
    public void VisibleText()
    {
        Color promptColor = PromptText.color;
        Color inputColor = InputText.color;
        promptColor.a = inputColor.a = 1f;
        PromptText.color = promptColor;
        InputText.color = inputColor;
    }
}