using System.Threading;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D col;
    private bool canJump = false;
    public float jumpForce;
    public bool isBugged;
    private bool afterBug;

    public WaveScript waveScript;

    private void Start()
    {
        if(waveScript == null)
        {
            waveScript = GetComponent<WaveScript>();
        }
    }


    void Update()
    {
        BugEvent();
        if (isBugged)
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0f;
            afterBug = true;
        }
        else
        {
            if(afterBug)
            {
                afterBug = false;
                rb.gravityScale = 1f;
                transform.position = new Vector2(-7f, -0.49f);
                DeletePrefabs();
            }
            rb.gravityScale = 1f;
            if (Input.GetKey(KeyCode.UpArrow) && canJump)
            {
                Jump();
            }

            if (Input.GetKey(KeyCode.DownArrow))
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
        }
    }

    private void BugEvent()
    {
        if(waveScript.totalElapsedTime % 10f < 1f && waveScript.totalElapsedTime > 1f && !isBugged)
        {
            //int rand = Random.Range(0, 5);
            //Debug.Log("HIT! RAND : " + rand);
            //if(rand == 4)
            //{
                isBugged = true;
            //}
        }
    }

    private void Jump()
    {
        Debug.Log("key was pressed");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        canJump = false;
    }

    private void Duck()
    {
        Vector3 newScale = transform.localScale;
        newScale.x = 1.3f;
        newScale.y = 0.4f;
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
            Debug.Log("Can jump loh");
            canJump = true;
        }
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("GAME OVER!!!");
        }
    }
}
