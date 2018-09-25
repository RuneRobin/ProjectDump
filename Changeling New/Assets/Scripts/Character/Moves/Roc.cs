using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roc : MonoBehaviour
{
    #region Variables
    #region Jumping
    private readonly int jumpHeight = 100; //The height in pixels that the first jump will provide
    private bool jumpActive = false; //If the chacrter is currently jumping
    private bool canJump = false; //If the character is able to jump again, this reset when touching the floor.
    #endregion
    #endregion

    #region MonoBehavior Methods
    /// <summary>
    /// Monobehaivor function that is called every frame.
    /// </summary>
    public void Update()
    {
        //Jumping
        if (Input.GetKeyDown("Jump"))
        {
            Jump();
        }
    }
    #endregion

    #region Jump Methods
    /// <summary>
    /// Checks is the charcter is able to jump and if so causes the jump action.
    /// </summary>
    private void Jump()
    {
        if (jumpActive == false) //If the chacrter is not already jumping
        {
            if (canJump) //If the character can jump again
            {
                MultiJump(5); //Call the MultiJump function with the desired number of jumps.
            }
        }
    }

    /// <summary>
    /// Causes the character to be able to jump x number of times, each jump will have diminishing returns.
    /// </summary>
    /// <param name="_jumps"></param>
    private void MultiJump(int _jumps)
    {
        int maxJumps = _jumps;
        int dropOff = (jumpHeight / maxJumps); //The ammount off drop off each jump will have (deminishing returns)
        jumpActive = true; //Sets jumping to true

        while (jumpActive == true) //Check to make sure the character has not already landed.
        {
            if (_jumps > 0) //Check to see if the character has any jumps remaining
            {
                if (Input.GetKeyDown("Jump")) 
                {
                    //
                    // Jump Code Here (chacacter height += (jumpHeight -= (dropOff * (maxJumps - _jump_)))
                    //
                    _jumps--;
                }
            }
            else //If the chacrter has no remaining jumps
            {
                jumpActive = false;
            }
        }
    }
    #endregion
}
