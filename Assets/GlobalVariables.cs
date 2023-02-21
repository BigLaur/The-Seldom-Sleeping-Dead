using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    //Sets the actions the player can do
    public static int actions;
    public static bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        actions = 2;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
