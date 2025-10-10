using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D Rb;
    public BoxCollider2D Col;
    private bool _canJump = true;
    private bool _offGround = false;
    public bool IsAlive = true;
    public float JumpForce = 13.5f;
    public bool IsBugged = false;
    private bool _afterBug;

    public KeyCode JumpKey = KeyCode.UpArrow;
    public KeyCode DuckKey = KeyCode.DownArrow;

    public WaveScript WaveScript;
    public AudioManagerScript AudioScript;

    private void Start()
    {
        if(WaveScript == null)
        {
            WaveScript = GetComponent<WaveScript>();
        }
        if(AudioScript == null) AudioScript = GameObject.Find("Audio Manager").GetComponent<AudioManagerScript>();
    }


    void Update()
    {
        BugEvent();

        //if (isBugged)
        //{
        //    rb.velocity = Vector3.zero;
        //    rb.gravityScale = 0f;
        //    afterBug = true;
        //}
        //else
        //{
        //    if(afterBug)
        //    {
        //        afterBug = false;
        //        rb.gravityScale = 5f;
        //        transform.position = new Vector2(-7f, -0.49f);
        //        DeletePrefabs();
        //    }

            Rb.gravityScale = 5f;

            if (Input.GetKeyDown(JumpKey) && _canJump)
            {
                Jump();
                AudioScript.PlaySfx(AudioScript.Jump);
            }

            if (Input.GetKey(DuckKey) && !_offGround)
            {
                Duck();
            }
            else
            {
                Vector3 newScale = transform.localScale;
                newScale.x = 1f;
                newScale.y = 1f;
                transform.localScale = newScale;
            }
        //}
    }

    private void BugEvent()
    {
        if(WaveScript.TotalElapsedTime % 15f < 1f && WaveScript.TotalElapsedTime > 1f && !IsBugged)
        {
            //int rand = Random.Range(0, 5);
            //Debug.Log("HIT! RAND : " + rand);
            //if(rand == 4)
            //{
                IsBugged = true;
            //}
        }
    }

    private void Jump()
    {
        //Debug.Log("key was pressed");
        Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
        _canJump = false;
        _offGround = true;
    }

    private void Duck()
    {
        Vector3 newScale = transform.localScale;
        newScale.x = 1.3f;
        newScale.y = 0.4f;
        transform.position = new Vector2(transform.position.x, -0.8f);
        transform.localScale = newScale;
    }

    private void DeletePrefabs()
    {
        GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obj in prefabs)
        {
            Destroy(obj);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Road")
        {
            //Debug.Log("Can jump loh");
            _offGround = false;
            _canJump = true;
        }
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            IsAlive = false;
            AudioScript.MusicSource.Stop();
            AudioScript.PlaySfx(AudioScript.Death);
            Debug.Log("GAME OVER!!!");
            SceneManager.LoadScene(2);
        }
    }
}
