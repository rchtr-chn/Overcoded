using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    public GameObject StartManager;
    public Text pressSpace;
    public Transform Target;
    public float zoomingSpeed = 2f;

    private bool isZooming = false;
    private Camera mainCamera;
    private float initialSize;
    private Vector3 initialPosition;

    void Start()
    {
        mainCamera = Camera.main;
        initialSize = mainCamera.orthographicSize;
        initialPosition = mainCamera.transform.position;
        StartManager.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isZooming)
        {
            Debug.Log("Space Pressed");
            StartCoroutine(ZoomIn());
        }
    }

    IEnumerator ZoomIn()
    {
        isZooming = true;
        pressSpace.enabled = false;

        Vector3 targetPosition = new Vector3(Target.position.x, Target.position.y, mainCamera.transform.position.z - 5f);
        float targetSize = initialSize * 0.6f;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * zoomingSpeed;
            mainCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            mainCamera.orthographicSize = Mathf.Lerp(initialSize, targetSize, t);
            yield return null;
        }

        StartManager.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}