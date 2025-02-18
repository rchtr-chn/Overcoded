using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D col;
    private bool canJump = false;
    public float jumpForce;
    public bool isBugged;
    private Vector3 tempForce;

    void Update()
    {
        // IF THERE IS A BUG.....
        if (isBugged)
        {
            tempForce = rb.velocity;
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0f;
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
            rb.velocity = tempForce;
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
                col.offset = new Vector2(col.offset.x, 0);
                col.size = new Vector2(col.size.x, 1);
            }
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
        col.offset = new Vector2(col.offset.x, -0.3f);
        col.size = new Vector2(col.size.x, 0.3f);
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
