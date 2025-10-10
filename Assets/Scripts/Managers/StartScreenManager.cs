using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    public GameObject StartMenu;
    public GameObject OptionsMenu;

    public Text PressSpace;
    public Transform Target;
    public Transform OptionsTarget;
    public float ZoomingSpeed;  // Speed for zooming in
    public float ZoomingSpeed2; // Speed for returning to menu

    private bool _isZooming = false;
    private Camera _mainCamera;
    private float _initialSize;
    private Vector3 _initialPosition;
    private float _targetSize;

    void Start()
    {
        _mainCamera = Camera.main;
        _initialSize = _mainCamera.orthographicSize;
        _initialPosition = _mainCamera.transform.position;
        StartMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        _targetSize = _initialSize * 0.6f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isZooming)
        {
            //Debug.Log("Space Pressed");
            StartCoroutine(ZoomIn(Target, true, ZoomingSpeed));
        }
    }

    IEnumerator ZoomIn(Transform newTarget, bool showStartMenu, float speed)
    {
        _isZooming = true;
        PressSpace.enabled = false;

        Vector3 targetPosition = new Vector3(newTarget.position.x, newTarget.position.y, _mainCamera.transform.position.z - 5f);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * speed;
            _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, targetPosition, t);
            _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, _targetSize, t);
            yield return null;
        }

        StartMenu.SetActive(showStartMenu);
        OptionsMenu.SetActive(!showStartMenu);

        _isZooming = false;
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
        if (!_isZooming)
        {
            StartCoroutine(ZoomIn(OptionsTarget, false, ZoomingSpeed));
        }
    }

    public void BackToMenu()
    {
        if (!_isZooming)
        {
            StartCoroutine(ZoomIn(Target, true, ZoomingSpeed2));
        }
    }
}
