using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FlyScript : MonoBehaviour
{
    public FlySpawnerScript flySpawnerScript;
    public WaveScript waveScript;

    [Header("Input Handler")]
    private Camera _mainCamera;

    [Header("Monitor Bounds")]
    [SerializeField] private GameObject monitorBounds1;
    [SerializeField] private GameObject monitorBounds2;

    [Header("Screen Bounds & Fly Size")]
    private Vector2 screenBounds;
    private float objectHeight;
    private float objectWidth;

    [Header("Fly Movement")]
    private Rigidbody2D rb; 
    private float angle = 0f;
    public float speed;
    public float directionChangeInterval = 2f; 
    public float frequency = 1f;
    public float magnitude = 0.5f; 
    public float avoidanceRadius = 2f; 

    [Header("Monitor Bounds")]
    private Vector3 direction;
    private Vector3 center1, center2;
    private bool circlingFirstRound = true;
    private float radius1, radius2;

    [Header("Floating Text")]
    [SerializeField] private TextMeshProUGUI floatingTextPrefab;
    [SerializeField] private float duration = 5f;

    [Header("UI Elements")]
    [SerializeField] private Canvas canvas;

    void Start() 
    {
        flySpawnerScript = GameObject.Find("Fly-Spawner").GetComponent<FlySpawnerScript>();
        waveScript = GameObject.Find("Player").GetComponent<WaveScript>();
        rb = GetComponent<Rigidbody2D>();

        GetBounds();
       
        InvokeRepeating("ChangeDirection", 0, directionChangeInterval);
    }

    void Update() 
    {
        Move();
        GetBounds();
    }

    void LateUpdate()
    {
        Vector3 currentPos = transform.position;
        bool flipped = false;

        float buffer = 0.5f;

        if (currentPos.x > screenBounds.x + buffer || currentPos.x < screenBounds.x * -1 - buffer)
        {
            direction.x = -direction.x;
            flipped = true;
        }

        if (currentPos.y > screenBounds.y + buffer || currentPos.y < screenBounds.y * -1 - buffer)
        {
            direction.y = -direction.y;
            flipped = true;
        }

        if (flipped)
        {
            Flip();
        }

        currentPos.x = Mathf.Clamp(currentPos.x, screenBounds.x * -1 - buffer, screenBounds.x + buffer);
        currentPos.y = Mathf.Clamp(currentPos.y, screenBounds.y * -1 - buffer, screenBounds.y + buffer);
        transform.position = currentPos;
    }

    private void GetBounds()
    {
        _mainCamera = Camera.main;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        monitorBounds1 = GameObject.Find("Monitor1(Clone)");
        monitorBounds2 = GameObject.Find("Monitor2(Clone)");

        center1 = monitorBounds1.transform.position;
        radius1 = monitorBounds1.GetComponent<CircleCollider2D>().radius;

        if(waveScript.currentWave > 7)
        {
            center2 = monitorBounds2.transform.position;
            radius2 = monitorBounds2.GetComponent<CircleCollider2D>().radius;
        }
        
    }

    private void ChangeDirection()
    {
        direction = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0).normalized;
        speed = UnityEngine.Random.Range(7f, 15f);
        Flip();
    }

    private void Move()
    {
        Vector3 avoidanceDirection = GetAvoidanceDirection();
        rb.velocity = (direction + avoidanceDirection) * speed;

        angle += speed * Time.deltaTime;
        if(waveScript.currentWave < 8)
        {
            float x = Mathf.Cos(angle * frequency) * (radius1);
            float y = Mathf.Sin(angle * frequency) * (radius1);
        }
        else
        {
            float x = Mathf.Cos(angle * frequency) * (circlingFirstRound ? radius1 : radius2);
            float y = Mathf.Sin(angle * frequency) * (circlingFirstRound ? radius1 : radius2);
        }
        

        if(circlingFirstRound)
        {
            transform.position = Vector3.MoveTowards(transform.position, center1, speed * Time.deltaTime);
            if (transform.position == center1)
            {
                circlingFirstRound = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, center2, speed * Time.deltaTime);
            if (transform.position == center2)
            {
                circlingFirstRound = true;
            }
        }
    }

    private Vector3 GetAvoidanceDirection()
    {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0;

        Vector3 toCursor = cursorPosition - transform.position;
        if (toCursor.magnitude < avoidanceRadius)
        {
            return -toCursor.normalized;    
        }
        return Vector3.zero; 
    }

    private void Flip()
    {
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);
        if (!rayHit.collider) return;

        if (rayHit.collider.gameObject == gameObject)
        {
            Debug.Log("Hit!");
            Vector2 canvasPosition = WorldToCanvasPosition(gameObject.transform.position);

            TextMeshProUGUI floatingText = Instantiate(floatingTextPrefab, canvas.transform);
            floatingText.text = "Killed!";
            floatingText.GetComponent<RectTransform>().anchoredPosition = canvasPosition;

            Destroy(floatingText.gameObject, duration);

            flySpawnerScript.isActive = false;
            Destroy(gameObject);
        }
    }

    void AdjustSpeed()
    {
        if (waveScript.currentWave == 3)
        {
            speed = 2f;
        }
        else if (waveScript.currentWave == 8)
        {
            speed = 3f;
        }
        else if(waveScript.currentWave == 9)
        {
            speed = 4f;
        }
        else if(waveScript.currentWave > 9)
        {
            speed = 5f + 1 * waveScript.currentWave - 10;
        }
    }

    private Vector2 WorldToCanvasPosition(Vector3 worldPosition)
    {
        return _mainCamera.WorldToScreenPoint(worldPosition);
    }
}