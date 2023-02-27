using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCollide : MonoBehaviour
{
    static public bool cancelInput = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move.rb.position.y <= -1.5)
        {
            cancelInput = false;
            print("b");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (move.anim.GetBool("isJumping") == true)
        {
            print("a");
            
            cancelInput = true;
            move.dirx = 0;
            move.anim.SetBool("isJumping", false);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (move.anim.GetBool("isJumping") == true)
        {
            print("a");

            cancelInput = true;
            move.dirx = 0;
            move.anim.SetBool("isJumping", false);
        }
    }

}
