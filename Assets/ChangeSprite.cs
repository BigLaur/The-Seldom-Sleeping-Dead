using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    private SpriteRenderer rend;
    private Sprite damagedSprite, normalSprite;
    private bool canBeFixed = false;
    public bool isDamaged;
    public int remainingActions;
    public Text counterText;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        normalSprite = Resources.Load<Sprite>("grave_normal");
        damagedSprite = Resources.Load<Sprite>("grave_destroyed");
        if (isDamaged)
        {
            rend.sprite = damagedSprite;
        }
        else
        {
           rend.sprite = normalSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        remainingActions = GlobalVariables.actions;
        if (canBeFixed)
        {
            //code for outline

            //code to detect keypress
            if (Input.GetKeyDown(KeyCode.X))
            {
                isDamaged = false;
                //player crouches
                //transition
                //sound effects
                //rend.sprite = normalSprite;
                canBeFixed = false;
                GlobalVariables.actions -= 1;
            }
        }

        if (isDamaged)
        {
            rend.sprite = damagedSprite;
        }
        else
        {
            rend.sprite = normalSprite;
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
            //hide prompt
        }
    }
}
