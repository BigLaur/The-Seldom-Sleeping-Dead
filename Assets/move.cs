using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move : MonoBehaviour
{
    public static Rigidbody2D rb;
    public static Animator anim;
    float dirx, moveSpeed = 8f, crouchSpeed = 5f;
    public static bool isRunning, isCrouching;
    bool facingRight = true;
    Vector3 localScale;
    //public GameObject blackOutSquare;


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
        if (GlobalVariables.canMove)
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

            //StartCoroutine(RepairAnim());
            //StartCoroutine(FadeBlackOutSquare());
            //play crouch animation
            //anim.SetBool("isCrouching", true);
            //fade screen to black
            //play sound effect
            //fade back in
            //play stand up animation
            //anim.SetBool("isCrouching", false);
    }
    //IEnumerator RepairAnim()
    //{
    //    //Print the time of when the function is first called.
    //    //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        
    //    //play crouch animation
    //    anim.SetBool("isCrouching", true);

    //    //yield on a new YieldInstruction that waits for 5 seconds.
    //    yield return new WaitForSeconds(4);

    //    //fade screen to black
    //    StartCoroutine(FadeBlackOutSquare());
    //    //play sound effect
    //    //fade back in
    //    StartCoroutine(FadeBlackOutSquare(false));
    //    //play stand up animation
    //    anim.SetBool("isCrouching", false);

    //    //After we have waited 5 seconds print the time again.
    //    //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    //    GlobalVariables.canMove = true;
    //}
    //public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 2)
    //{
    //    Color objectColor = blackOutSquare.GetComponent<Image>().color;
    //    float fadeAmount;

    //    //play crouch animation
    //    anim.SetBool("isCrouching", true);

    //    while (blackOutSquare.GetComponent<Image>().color.a < 1)
    //    {
    //        fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

    //        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
    //        blackOutSquare.GetComponent<Image>().color = objectColor;
    //        yield return null;
    //    }
    //    while (blackOutSquare.GetComponent<Image>().color.a > 0)
    //    {
    //        fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

    //        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
    //        blackOutSquare.GetComponent<Image>().color = objectColor;
    //        yield return null;
    //    }

    //    //play stand up animation
    //    anim.SetBool("isCrouching", false);

    //    GlobalVariables.canMove = true;

    //    //if (fadeToBlack)
    //    //{
    //    //    while (blackOutSquare.GetComponent<Image>().color.a < 1)
    //    //    {
    //    //        fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

    //    //        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
    //    //        blackOutSquare.GetComponent<Image>().color = objectColor;
    //    //        yield return null;
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    while (blackOutSquare.GetComponent<Image>().color.a > 0)
    //    //    {
    //    //        fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

    //    //        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
    //    //        blackOutSquare.GetComponent<Image>().color = objectColor;
    //    //        yield return null;
    //    //    }
    //    //}
    //    //yield return new WaitForEndOfFrame();
    //}
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
