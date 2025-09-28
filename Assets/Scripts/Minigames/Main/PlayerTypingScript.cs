using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTypingScript : MonoBehaviour
{
    //WORDBANK NEEDED~!!!!
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

    public Text promptText;
    public Text inputText;
    public PlayerMovementScript playerMovementScript;
    public ObstacleSpawnerScript obstacleSpawnerScript;
    public AudioManagerScript audioScript;
    public float timer, timerCap;
    private bool bugAfflicted = false;
    private int bugIndex = -1;

    private string currentPrompt = string.Empty;
    private string currentInput = string.Empty;
    void Start()
    {
        //timerCap = 10f;
        //initialize coding minigame, CHANGE WHEN WORDBANK IS ADDED
        if (audioScript == null) audioScript = GameObject.Find("Audio Manager").GetComponent<AudioManagerScript>();
        obstacleSpawnerScript = GameObject.Find("Obstacle-Spawner").GetComponent<ObstacleSpawnerScript>();
        GetNewPrompt(wordBank[Random.Range(0, wordBank.Count())]);
        InvisibleText();
    }

    void Update()
    {
        if(playerMovementScript.isBugged)
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
            if(!bugAfflicted)
            {
                AfflictDebuffs();
                bugAfflicted = true;
            }

            VisibleText();
            TypeInput();
        }
    }

    private void AfflictDebuffs()
    {
        bugIndex = Random.Range(0, 2);

        switch (bugIndex)
        {
            case 0:
                Debug.Log("Afflicting debuff: Increase obstacle speed");
                // Example debuff: Increase obstacle speed
                obstacleSpawnerScript.prefabSpeed += 2f;
                break;
            case 1:
                Debug.Log("Afflicting debuff: Invert player controls");
                // Example debuff: invert player controls
                (playerMovementScript.jumpKey, playerMovementScript.duckKey) =
                    (playerMovementScript.duckKey, playerMovementScript.jumpKey);
                break;
            case 2:
                Debug.Log("Afflicting debuff: Decrease gravity");
                // Example debuff: Decrease gravity
                playerMovementScript.rb.gravityScale -= 1f;
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
                obstacleSpawnerScript.prefabSpeed -= 2f;
                break;
            case 1:
                // Revert player controls
                (playerMovementScript.jumpKey, playerMovementScript.duckKey) =
                    (playerMovementScript.duckKey, playerMovementScript.jumpKey);
                break;
            case 2:
                // Revert gravity change
                playerMovementScript.rb.gravityScale += 1f;
                break;
            default:
                // Default case if needed
                break;
        }
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
            audioScript.PlaySfx(audioScript.typing);

            if (c == '\n' || c == '\r')
            {
                timer = 0f;
                if(!CodeValidity())
                {
                    audioScript.PlaySfx(audioScript.incorrect);
                    Debug.Log("bugIndex =" + bugIndex);
                }
                else
                {
                    Debug.Log("bugIndex =" + bugIndex);
                    if (bugAfflicted || bugIndex != -1)
                    {
                        Debug.Log("bug removed!");
                        RevertDebuffs(bugIndex);
                        bugAfflicted = false;
                    }

                    bugIndex = -1;
                    audioScript.PlaySfx(audioScript.correct);
                }


                currentInput = "";
                inputText.text = "";
                InvisibleText();
                playerMovementScript.isBugged = false;

                GetNewPrompt(wordBank[Random.Range(0, wordBank.Count())]);
                return;
            }
            else if (c == '\b')
            {
                if(currentInput.Length > 0)
                {
                    currentInput = currentInput.Substring(0, currentInput.Length - 1);
                    inputText.text = currentInput;
                }
                return;
            }

            if (currentInput.Length < currentPrompt.Length)
            {
                currentInput += c;
                inputText.text = currentInput;
            }

        }
    }

    public bool CodeValidity()
    {
        if(currentInput == currentPrompt)
        {
            return true;
        }
        return false;
    }
    public void InvisibleText()
    {
        Color promptColor = promptText.color;
        Color inputColor = inputText.color;
        promptColor.a = inputColor.a = 0f;
        promptText.color = promptColor;
        inputText.color = inputColor;
    }
    public void VisibleText()
    {
        Color promptColor = promptText.color;
        Color inputColor = inputText.color;
        promptColor.a = inputColor.a = 1f;
        promptText.color = promptColor;
        inputText.color = inputColor;
    }
}