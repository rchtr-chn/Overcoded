using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ChatBubbleScript : MonoBehaviour
{
    public enum DragDirection { Left, Right, Up, Down }
    public DragDirection allowedDirection;
    public float fadeSpeed = 1f;

    private Vector3 startPos;
    private bool isDragging = false;
    private Vector3 dragStartWorldPos;
    private SpriteRenderer spriteRenderer;
    private bool isFading = false;
    private Vector3 offset;

    public List<Transform> targetPositions;
    public float moveDuration = 1f;
    private static List<Vector3> occupiedPositions = new List<Vector3>();
    bool isFree = true;

    public ChatBubbleSpawnerScript chatBubbleSpawnerScript;

    void Start()
    {
        GetTargetPos();

        if(spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        chatBubbleSpawnerScript = GameObject.Find("Cell-Phone").GetComponent<ChatBubbleSpawnerScript>();

        StartCoroutine(FadeIn());

        Vector3 availablePosition = FindAvailablePosition();
        if (availablePosition != Vector3.zero)
        {
            isFree = false;
            chatBubbleSpawnerScript.availablePos--;
            MoveToTarget(availablePosition);
        }
    }

    private void Update()
    {
        if(isFree)
        {
            Vector3 availablePosition = FindAvailablePosition();
            if (availablePosition != Vector3.zero)
            {
                isFree = false;
                chatBubbleSpawnerScript.availablePos--;
                MoveToTarget(availablePosition);
            }
        }
    }

    private void GetTargetPos()
    {
        targetPositions.Add(GameObject.Find("Chat-Pos-1").GetComponent<Transform>());
        targetPositions.Add(GameObject.Find("Chat-Pos-2").GetComponent<Transform>());
        targetPositions.Add(GameObject.Find("Chat-Pos-3").GetComponent<Transform>());
        targetPositions.Add(GameObject.Find("Chat-Pos-4").GetComponent<Transform>());
    }

    void OnMouseDown()
    {
        if (isFading) return;

        startPos = Input.mousePosition;
        dragStartWorldPos = transform.position;

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(startPos.x, startPos.y, Camera.main.WorldToScreenPoint(transform.position).z));
        offset = transform.position - worldMousePos;

        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (!isDragging || isFading) return;

        Vector3 currentMousePos = Input.mousePosition;
        Vector3 dragVector = currentMousePos - startPos;

        switch (allowedDirection)
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
        if (isFading) return;

        isDragging = false;
        Vector3 endPos = Input.mousePosition;
        Vector3 direction = endPos - startPos;

        bool correctDirection = false;

        switch (allowedDirection)
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
        worldMousePos += offset;
        worldMousePos.z = transform.position.z;

        transform.position = worldMousePos;
    }
    private Vector3 FindAvailablePosition()
    {
        foreach (Transform target in targetPositions)
        {
            if (!occupiedPositions.Contains(target.position))
            {
                occupiedPositions.Add(target.position);
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

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    private IEnumerator FadeIn()
    {
        isFading = true;
        float alpha = 0f;
        spriteRenderer.color = new Color(1f, 1f, 1f, alpha);

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        isFading = false;
    }

    private IEnumerator FadeOutAndDestroy()
    {
        isFading = true;
        float alpha = 1f;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        Destroy(gameObject);
        chatBubbleSpawnerScript.availablePos++;
    }
}
