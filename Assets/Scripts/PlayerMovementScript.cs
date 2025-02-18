using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D col;
    private bool canJump = false;
    public float jumpForce;

    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow) && canJump)
        {
            Jump();
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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Road")
        {
            Debug.Log("Can jump loh");
            canJump = true;
        }
    }
}
