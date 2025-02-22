using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    public GameObject StartManager;
    public GameObject OptionsMenu; 

    public Text pressSpace;
    public Transform Target;
    public Transform OptionsTarget;
    public float zoomingSpeed;

    private bool isZooming = false;
    private Camera mainCamera;
    private float initialSize;
    private Vector3 initialPosition;
    private float targetSize;

    void Start()
    {
        mainCamera = Camera.main;
        initialSize = mainCamera.orthographicSize;
        initialPosition = mainCamera.transform.position;
        StartManager.SetActive(false);
        OptionsMenu.SetActive(false);
        targetSize = initialSize * 0.6f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isZooming)
        {
            Debug.Log("Space Pressed");
            StartCoroutine(ZoomIn(Target, true));
        }
    }

    IEnumerator ZoomIn(Transform newTarget, bool showStartMenu)
    {
        isZooming = true;
        pressSpace.enabled = false;

        Vector3 targetPosition = new Vector3(newTarget.position.x, newTarget.position.y, mainCamera.transform.position.z - 5f);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * zoomingSpeed;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, t);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, t);
            yield return null;
        }

        StartManager.SetActive(showStartMenu);
        OptionsMenu.SetActive(!showStartMenu);

        isZooming = false;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        if (!isZooming)
        {
            StartCoroutine(ZoomIn(OptionsTarget, false));
        }
    }

    public void BackToMenu()
    {
        if (!isZooming)
        {
            StartCoroutine(ZoomIn(Target, true));
        }
    }
}
