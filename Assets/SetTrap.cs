using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Uses UI
using UnityEngine.UI;

public class SetTrap : MonoBehaviour
{
    public List<GameObject> traps;
    public int remainingActions;
    bool ignoreInput = false;
    public GameObject blackOutSquare;
    private string trapToSet;
    private GameObject toSpawn;
    // Start is called before the first frame update
    void Start()
    {
        trapToSet = "trap";
        for (int i = 0; i < traps.Count; i++)
        {
            if (traps[i].name == trapToSet)
            {
                toSpawn = traps[i];
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        remainingActions = GlobalVariables.actions;
        if (move.rb.velocity.y == 0 && move.rb.velocity.x == 0 && move.isCrouching == false && remainingActions > 0)
        {
            // Detects if button pressed, ignoreInput stops player from spamming
            if (Input.GetKeyDown(KeyCode.C) && ignoreInput == false)
            {
                // So player can't repeat input during transition
                ignoreInput = true;
                // Disables player's movement
                GlobalVariables.canMove = false;
                StartCoroutine(PlaceTrap());
            }
        }
    }
    public IEnumerator PlaceTrap(int fadeSpeed = 2)
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
        // Get player's position
        Vector2 pos = new Vector2(move.rb.position.x, move.rb.position.y - 1f);
        // Spawn our trap object
        Instantiate(toSpawn, pos, toSpawn.transform.rotation);
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
