using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairGrave : MonoBehaviour
{
    //private SpriteRenderer rend;
    //private Material mat;
    //private Sprite damagedSprite, normalSprite, damagedOutlinedSprite;
    private Animator anim;
    private bool canBeFixed = false;
    public bool isDamaged;
    public int remainingActions;
    public Text counterText;
    public GameObject blackOutSquare;

    // Start is called before the first frame update
    void Start()
    {
        //rend = GetComponent<SpriteRenderer>();

        //mat = GetComponent<Material>();
        //mat = Resources.Load<Material>("Grave");

        anim = GetComponent<Animator>();

        //normalSprite = Resources.Load<Sprite>("grave_normal");
        //damagedSprite = Resources.Load<Sprite>("grave_destroyed");

        //normalSprite = Resources.Load<Sprite>("spritesheet_0");
        //damagedSprite = Resources.Load<Sprite>("spritesheet_1");
        //damagedOutlinedSprite = Resources.Load<Sprite>("spritesheet_2");

        if (isDamaged && !canBeFixed)
        {
            //rend.sprite = damagedSprite;
            anim.SetBool("isDamaged", true);
            anim.SetBool("isHovered", false);
        }
        else if (isDamaged && canBeFixed)
        {
            //rend.sprite = damagedOutlinedSprite;
            anim.SetBool("isDamaged", true);
            anim.SetBool("isHovered", true);
        }
        else
        {
            //rend.sprite = normalSprite;
            anim.SetBool("isDamaged", false);
            anim.SetBool("isHovered", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        remainingActions = GlobalVariables.actions;
        if (canBeFixed && move.rb.velocity.y == 0 && move.isCrouching == false)
        {
            //code for outline

            //code to detect keypress
            if (Input.GetKeyDown(KeyCode.X))
            {
                //player crouches
                GlobalVariables.canMove = false;
                //transition
                StartCoroutine(Repair());
                //sound effects
                //rend.sprite = normalSprite;
                GlobalVariables.actions -= 1;
            }
        }

        if (isDamaged && !canBeFixed)
        {
            //rend.sprite = damagedSprite;
            anim.SetBool("isDamaged", true);
            anim.SetBool("isHovered", false);
        }
        else if (isDamaged && canBeFixed)
        {
            //rend.sprite = damagedOutlinedSprite;
            anim.SetBool("isDamaged", true);
            anim.SetBool("isHovered", true);
        }
        else
        {
            //rend.sprite = normalSprite;
            anim.SetBool("isDamaged", false);
            anim.SetBool("isHovered", false);
        }
        counterText.text = remainingActions.ToString();
    }

    //When player enters area around grave
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isDamaged == true && remainingActions > 0)
        {
            print("trigger entered");
            canBeFixed = true;

            //Color color = mat.color;
            //color.a = Mathf.Clamp(1, 0, 1);
            ////rend.material.color = color;
            //mat.color = color;

            //show prompt
        }
    }
    //When player leaves area around grave
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("trigger exited");
            canBeFixed = false;

            ////Color color = rend.material.color;
            ////color.a = Mathf.Clamp(1, 1, 0);
            ////rend.material.color = color;
            //Color color = mat.color;
            //color.a = Mathf.Clamp(1, 1, 0);
            ////rend.material.color = color;
            //mat.color = color;

            //hide prompt
        }
    }
    public IEnumerator Repair(int fadeSpeed = 2)
    {
        //Gets the color/alpha of the UI screen blackout image so it can be manipulated
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        //play crouch animation
        move.anim.SetBool("isCrouching", true);
        //changes the alpha of the UI screen blackout image to 1 (opache) over fadeSpeed amount of seconds
        while (blackOutSquare.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackOutSquare.GetComponent<Image>().color = objectColor;
            yield return null;
        }
        //pause on black screen for 1 sec
        yield return new WaitForSeconds(1);
        canBeFixed = false;
        isDamaged = false;
        //changes the alpha of the UI screen blackout image to 0 (transparent) over fadeSpeed amount of seconds
        while (blackOutSquare.GetComponent<Image>().color.a > 0)
        {
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackOutSquare.GetComponent<Image>().color = objectColor;
            yield return null;
        }

        //play stand up animation
        move.anim.SetBool("isCrouching", false);

        //Restore player's ability to move
        GlobalVariables.canMove = true;
    }
}
