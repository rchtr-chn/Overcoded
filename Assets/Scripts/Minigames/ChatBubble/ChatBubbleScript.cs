using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ChatBubbleScript : MonoBehaviour
{
    public enum DragDirection { Left, Right, Up, Down }
    public List<Sprite> Sprites = new List<Sprite>();
    public DragDirection AllowedDirection;
    public float FadeSpeed = 1f;

    private Vector3 _startPos;
    private bool _isDragging = false;
    private Vector3 _dragStartWorldPos;
    private SpriteRenderer _spriteRenderer;
    private bool _isFading = false;
    private Vector3 _offset;

    public List<Transform> TargetPositions;
    public float MoveDuration = 1f;
    private static List<Vector3> _occupiedPositions = new List<Vector3>();
    private bool _isFree = true;

    public ChatBubbleSpawnerScript ChatBubbleSpawnerScript;

    void Start()
    {
        AllowedDirection = GetRandomEnumValue<DragDirection>();

        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        switch (AllowedDirection)
        {
            case DragDirection.Left:
                _spriteRenderer.sprite = Sprites[2];
                break;
            case DragDirection.Right:
                _spriteRenderer.sprite = Sprites[3];
                break;
            case DragDirection.Up:
                _spriteRenderer.sprite = Sprites[0];
                break;
            case DragDirection.Down:
                _spriteRenderer.sprite = Sprites[1];
                break;
        }

        GetTargetPos();

        ChatBubbleSpawnerScript = GameObject.Find("Cell-Phone").GetComponent<ChatBubbleSpawnerScript>();

        StartCoroutine(FadeIn());

        Vector3 availablePosition = FindAvailablePosition();
        if (availablePosition != Vector3.zero)
        {
            _isFree = false;
            //chatBubbleSpawnerScript.availablePos--;
            MoveToTarget(availablePosition);
        }
    }

    private void Update()
    {
        if (_isFree)
        {
            Vector3 availablePosition = FindAvailablePosition();
            if (availablePosition != Vector3.zero)
            {
                _isFree = false;
                //chatBubbleSpawnerScript.availablePos--;
                MoveToTarget(availablePosition);
            }
        }
    }

    public static T GetRandomEnumValue<T>() where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }
    private void GetTargetPos()
    {
        TargetPositions.Add(GameObject.Find("Chat-Pos-1").GetComponent<Transform>());
        TargetPositions.Add(GameObject.Find("Chat-Pos-2").GetComponent<Transform>());
        TargetPositions.Add(GameObject.Find("Chat-Pos-3").GetComponent<Transform>());
        TargetPositions.Add(GameObject.Find("Chat-Pos-4").GetComponent<Transform>());
    }

    void OnMouseDown()
    {
        if (_isFading) return;

        _startPos = Input.mousePosition;
        _dragStartWorldPos = transform.position;

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(_startPos.x, _startPos.y, Camera.main.WorldToScreenPoint(transform.position).z));
        _offset = transform.position - worldMousePos;

        _isDragging = true;
    }

    void OnMouseDrag()
    {
        if (!_isDragging || _isFading) return;

        Vector3 currentMousePos = Input.mousePosition;
        Vector3 dragVector = currentMousePos - _startPos;

        switch (AllowedDirection)
        {
            case DragDirection.Left:
                if (dragVector.x < 0 && Mathf.Abs(dragVector.x) > Mathf.Abs(dragVector.y))
                    MoveObject(currentMousePos);
                break;
            case DragDirection.Right:
                if (dragVector.x > 0 && Mathf.Abs(dragVector.x) > Mathf.Abs(dragVector.y))
                    MoveObject(currentMousePos);
                break;
            case DragDirection.Up:
                if (dragVector.y > 0 && Mathf.Abs(dragVector.y) > Mathf.Abs(dragVector.x))
                    MoveObject(currentMousePos);
                break;
            case DragDirection.Down:
                if (dragVector.y < 0 && Mathf.Abs(dragVector.y) > Mathf.Abs(dragVector.x))
                    MoveObject(currentMousePos);
                break;
        }
    }

    void OnMouseUp()
    {
        if (_isFading) return;

        _isDragging = false;
        Vector3 endPos = Input.mousePosition;
        Vector3 direction = endPos - _startPos;

        bool correctDirection = false;

        switch (AllowedDirection)
        {
            case DragDirection.Left:
                correctDirection = direction.x < 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y);
                break;
            case DragDirection.Right:
                correctDirection = direction.x > 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y);
                break;
            case DragDirection.Up:
                correctDirection = direction.y > 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x);
                break;
            case DragDirection.Down:
                correctDirection = direction.y < 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x);
                break;
        }

        if (correctDirection)
        {
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    private void MoveObject(Vector3 mousePos)
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.WorldToScreenPoint(transform.position).z));
        worldMousePos += _offset;
        worldMousePos.z = transform.position.z;

        transform.position = worldMousePos;
    }
    private Vector3 FindAvailablePosition()
    {
        foreach (Transform target in TargetPositions)
        {
            if (!_occupiedPositions.Contains(target.position))
            {
                _occupiedPositions.Add(target.position);
                return target.position;
            }
        }
        return Vector3.zero;
    }
    private void MoveToTarget(Vector3 targetPosition)
    {
        StartCoroutine(MoveToTargetCoroutine(targetPosition));
    }

    IEnumerator MoveToTargetCoroutine(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < MoveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / MoveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    private IEnumerator FadeIn()
    {
        _isFading = true;
        float alpha = 0f;
        _spriteRenderer.color = new Color(1f, 1f, 1f, alpha);

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * FadeSpeed;
            _spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        _isFading = false;
    }

    private IEnumerator FadeOutAndDestroy()
    {
        _isFading = true;
        float alpha = 1f;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * FadeSpeed;
            _spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        Destroy(gameObject);
        _occupiedPositions.RemoveAt(0);
        ChatBubbleSpawnerScript.AvailablePos++;
    }
}
