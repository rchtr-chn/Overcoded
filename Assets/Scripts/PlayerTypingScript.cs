using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTypingScript : MonoBehaviour
{
    //WORDBANK NEEDED~!!!!
    private string[] wordBank =
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

    public Text promptText;
    public Text inputText;
    public PlayerMovementScript playerMovementScript;

    private string currentPrompt = string.Empty;
    private string currentInput = string.Empty;
    void Start()
    {
        //initialize coding minigame, CHANGE WHEN WORDBANK IS ADDED
        GetNewPrompt(wordBank[Random.Range(0, wordBank.Count())]);
    }

    void Update()
    {
        TypeInput();
    }

    private void GetNewPrompt(string word)
    {
        currentPrompt = word;
        promptText.text = currentPrompt;
    }

    private void TypeInput()
    {
        foreach (char c in Input.inputString)
        {

            if (c == '\n' || c == '\r')
            {
                if (CodeValidity())
                {
                    playerMovementScript.isBugged = false;

                    //CHECKS ERROR IN CODE INPUT
                }

                currentInput = string.Empty;
                inputText.text = currentInput;

                //GET NEW PROMPT HERE!!!!
                return;
            }
            else if (c == '\b' && currentInput.Length > 0)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                inputText.text = currentInput;
                return;
            }

            if (currentInput.Length < currentPrompt.Length)
            {
                currentInput = currentInput + c;
                inputText.text = currentInput;
            }

        }
    }

    private bool CodeValidity()
    {
        if(currentInput == currentPrompt)
        {
            return true;
        }
        return false;
    }
}