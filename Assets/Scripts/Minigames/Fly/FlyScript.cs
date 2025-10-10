using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class FlyScript : MonoBehaviour
{
    public FlySpawnerScript FlySpawnerScript;
    public WaveScript WaveScript;

    [Header("Input Handler")]
    private Camera _mainCamera;

    [Header("Monitor Bounds")]
    [SerializeField] private GameObject _monitorBounds1;
    [SerializeField] private GameObject _monitorBounds2;

    [Header("Screen Bounds & Fly Size")]
    private Vector2 _screenBounds;
    private float _objectHeight;
    private float _objectWidth;

    [Header("Fly Movement")]
    private Rigidbody2D _rb; 
    private float _angle = 0f;
    public float Speed = 2;
    public float DirectionChangeInterval = 2f; 
    public float Frequency = 1f;
    public float Magnitude = 0.5f; 
    public float AvoidanceRadius = 2f; 

    [Header("Monitor Bounds")]
    private Vector3 _direction;
    private Vector3 _center1, _center2;
    private bool _circlingFirstRound = true;
    private float _radius1, _radius2;

    [Header("Floating Text")]
    [SerializeField] private TextMeshProUGUI _floatingTextPrefab;
    [SerializeField] private float _duration = 5f;

    [Header("UI Elements")]
    [SerializeField] private Canvas _canvas;

    void Start() 
    {
        FlySpawnerScript = GameObject.Find("Fly-Spawner").GetComponent<FlySpawnerScript>();
        WaveScript = GameObject.Find("Player").GetComponent<WaveScript>();
        _rb = GetComponent<Rigidbody2D>();

        GetBounds();
       
        InvokeRepeating("ChangeDirection", 0, DirectionChangeInterval);
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

        if (currentPos.x > _screenBounds.x + buffer || currentPos.x < _screenBounds.x * -1 - buffer)
        {
            _direction.x = -_direction.x;
            flipped = true;
        }

        if (currentPos.y > _screenBounds.y + buffer || currentPos.y < _screenBounds.y * -1 - buffer)
        {
            _direction.y = -_direction.y;
            flipped = true;
        }

        if (flipped)
        {
            Flip();
        }

        currentPos.x = Mathf.Clamp(currentPos.x, _screenBounds.x * -1 - buffer, _screenBounds.x + buffer);
        currentPos.y = Mathf.Clamp(currentPos.y, _screenBounds.y * -1 - buffer, _screenBounds.y + buffer);
        transform.position = currentPos;
    }

    private void GetBounds()
    {
        _mainCamera = Camera.main;
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        _objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        _monitorBounds1 = GameObject.Find("Monitor1(Clone)");
        _monitorBounds2 = GameObject.Find("Monitor2(Clone)");

        _center1 = _monitorBounds1.transform.position;
        _radius1 = _monitorBounds1.GetComponent<CircleCollider2D>().radius;

        if(WaveScript.CurrentWave > 7)
        {
            _center2 = _monitorBounds2.transform.position;
            _radius2 = _monitorBounds2.GetComponent<CircleCollider2D>().radius;
        }
        
    }

    private void ChangeDirection()
    {
        _direction = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0).normalized;
        Speed = UnityEngine.Random.Range(7f, 15f);
        Flip();
    }

    private void Move()
    {
        Vector3 avoidanceDirection = GetAvoidanceDirection();
        _rb.velocity = (_direction + avoidanceDirection) * Speed;

        _angle += Speed * Time.deltaTime;
        if(WaveScript.CurrentWave < 8)
        {
            float x = Mathf.Cos(_angle * Frequency) * (_radius1);
            float y = Mathf.Sin(_angle * Frequency) * (_radius1);
        }
        else
        {
            float x = Mathf.Cos(_angle * Frequency) * (_circlingFirstRound ? _radius1 : _radius2);
            float y = Mathf.Sin(_angle * Frequency) * (_circlingFirstRound ? _radius1 : _radius2);
        }
        

        if(_circlingFirstRound)
        {
            transform.position = Vector3.MoveTowards(transform.position, _center1, Speed * Time.deltaTime);
            if (transform.position == _center1)
            {
                _circlingFirstRound = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _center2, Speed * Time.deltaTime);
            if (transform.position == _center2)
            {
                _circlingFirstRound = true;
            }
        }
    }

    private Vector3 GetAvoidanceDirection()
    {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0;

        Vector3 toCursor = cursorPosition - transform.position;
        if (toCursor.magnitude < AvoidanceRadius)
        {
            return -toCursor.normalized;    
        }
        return Vector3.zero; 
    }

    private void Flip()
    {
        if (_direction.x > 0)
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

            TextMeshProUGUI floatingText = Instantiate(_floatingTextPrefab, _canvas.transform);
            floatingText.text = "Killed!";
            floatingText.GetComponent<RectTransform>().anchoredPosition = canvasPosition;

            Destroy(floatingText.gameObject, _duration);

            FlySpawnerScript.IsActive = false;
            Destroy(gameObject);
        }
    }

    void AdjustSpeed()
    {
        if (WaveScript.CurrentWave == 3)
        {
            Speed = 2f;
        }
        else if (WaveScript.CurrentWave == 8)
        {
            Speed = 3f;
        }
        else if(WaveScript.CurrentWave == 9)
        {
            Speed = 4f;
        }
        else if(WaveScript.CurrentWave > 9)
        {
            Speed = 5f + 1 * WaveScript.CurrentWave - 10;
        }
    }

    private Vector2 WorldToCanvasPosition(Vector3 worldPosition)
    {
        return _mainCamera.WorldToScreenPoint(worldPosition);
    }
}