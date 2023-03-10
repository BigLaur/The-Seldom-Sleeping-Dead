using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    float dirx, moveSpeed = 8f, crouchSpeed = 5f;
    bool isRunning, isCrouching;
    bool facingRight = true;
    Vector3 localScale;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && rb.velocity.y == 0)
        {
            rb.AddForce(Vector2.up * 550f);
        }
        if (Input.GetKey(KeyCode.LeftShift) && rb.velocity.y == 0)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }

        SetAnimationState();
        if (isCrouching)
        {
            dirx = Input.GetAxisRaw("Horizontal") * crouchSpeed;
        }
        else
        {
            dirx = Input.GetAxisRaw("Horizontal") * moveSpeed;
        }
        

    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirx, rb.velocity.y);
    }
    void LateUpdate()
    {
        CheckWhereToFace();
    }
    void SetAnimationState()
    {
        if (dirx == 0)
        {
            anim.SetBool("isRunning", false);
        }
        if (rb.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
        }
        if (((Mathf.Abs(dirx) == moveSpeed) || (Mathf.Abs(dirx) == crouchSpeed)) && rb.velocity.y == 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if (isCrouching)
        {
            anim.SetBool("isCrouching", true);
        }
        else
        {
            anim.SetBool("isCrouching", false);
        }
        if (rb.velocity.y > 0)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isCrouching", false);
        }
    }
    void CheckWhereToFace()
    {
        if (dirx > 0)
        {
            facingRight = true;
        }
        else if (dirx < 0)
        {
            facingRight = false;
        }
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
    }

}
