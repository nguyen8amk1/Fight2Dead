using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour
{
    // Start is called before the first frame update
    public static int charId = -1;
    public static bool moveLeft = false;
    public static bool moveRight = false;
    //public static int facingDir = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(charId != -1)
		{
            if(charId == 1)
			{
                Player1.isBeingControlled = true;
                if(moveLeft)
				{
					Player1.moveLeft = moveLeft;
                    moveLeft = false;
				}
                if(moveRight)
				{
					Player1.moveRight = moveRight;
                    moveLeft = false;
				}
			} else if (charId == 2)
			{
                Player2.isBeingControlled = true;
                if(moveLeft)
				{
					Player2.moveLeft = moveLeft;
                    moveLeft = false;
				}
                if(moveRight)
				{
					Player2.moveRight = moveRight;
                    moveLeft = false;
				}
			}
		}
    }
}
