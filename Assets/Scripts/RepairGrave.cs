using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Uses UI
using UnityEngine.UI;

public class RepairGrave : MonoBehaviour
{
    // Grave sprites are set up as animations controlled through anim
    private Animator anim;
    private bool canBeFixed = false;
    public bool isDamaged;
    public int remainingActions;
    bool ignoreInput = false;
    public Text counterText;
    // Used to fade screen to/from black
    public GameObject blackOutSquare;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // Displays the correct sprite
        if (isDamaged && !canBeFixed)
        {
            // Sets sprite to damaged without outline
            anim.SetBool("isDamaged", true);
            anim.SetBool("isHovered", false);
        }
        else if (isDamaged && canBeFixed)
        {
            // Sets sprite to damaged with outline
            anim.SetBool("isDamaged", true);
            anim.SetBool("isHovered", true);
        }
        else
        {
            // Sets sprite to repaired
            anim.SetBool("isDamaged", false);
            anim.SetBool("isHovered", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Gets the number of actions the player has left (from the script GlobalVariables)
        remainingActions = GlobalVariables.actions;
        // If the player is stood still near a damaged grave
        if (canBeFixed && move.rb.velocity.y == 0 && move.rb.velocity.x == 0 && move.isCrouching == false)
        {
            // Detects if button pressed, ignoreInput stops player from spamming
            if (Input.GetKeyDown(KeyCode.X) && ignoreInput == false)
            {
                // So player can't repeat input during transition
                ignoreInput = true;
                // Disables player's movement
                GlobalVariables.canMove = false;
                StartCoroutine(Repair());
            }
        }
        // Updates the displayed sprite
        if (isDamaged && !canBeFixed)
        {
            anim.SetBool("isDamaged", true);
            anim.SetBool("isHovered", false);
        }
        else if (isDamaged && canBeFixed)
        {
            anim.SetBool("isDamaged", true);
            anim.SetBool("isHovered", true);
        }
        else
        {
            anim.SetBool("isDamaged", false);
            anim.SetBool("isHovered", false);
        }
        counterText.text = remainingActions.ToString();
    }

    // When player enters the area around the grave
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isDamaged == true && remainingActions > 0)
        {
            print("trigger entered"); //for debug
            canBeFixed = true;

            //show text prompt
        }
    }
    //When player leaves the area around the grave
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("trigger exited"); //for debug
            canBeFixed = false;

            //hide text prompt
        }
    }
    public IEnumerator Repair(int fadeSpeed = 2)
    {
        // Gets the color/alpha of the UI screen blackout image so it can be manipulated
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        // Play crouch animation
        move.anim.SetBool("isCrouching", true);
        // Changes the alpha of the UI screen blackout image to 1 (opache) over fadeSpeed amount of seconds
        while (blackOutSquare.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackOutSquare.GetComponent<Image>().color = objectColor;
            yield return null;
        }
        // Pause on black screen for 1 sec
        yield return new WaitForSeconds(1);
        // Update our variables
        canBeFixed = false;
        isDamaged = false;
        GlobalVariables.actions -= 1;
        // Changes the alpha of the UI screen blackout image to 0 (transparent) over fadeSpeed amount of seconds
        while (blackOutSquare.GetComponent<Image>().color.a > 0)
        {
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            blackOutSquare.GetComponent<Image>().color = objectColor;
            yield return null;
        }

        // Play stand up animation
        move.anim.SetBool("isCrouching", false);

        // Restore player's ability to move
        GlobalVariables.canMove = true;
        // Restore player's input
        ignoreInput = false;
    }
}
