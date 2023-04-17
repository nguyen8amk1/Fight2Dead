using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipChar : MonoBehaviour
{   
    public string _currentDirection = "left";
    public GameObject characters;

    private GameState playerInfo = GameState.Instance;

	private void Start()
	{
        flipChar();	
	}

	// Start is called before the first frame update
	public void flipChar()
    {

        Debug.Log("Flip character");
        bool isP1 = playerInfo.PlayerId == 1;
        bool isP2 = playerInfo.PlayerId == 2;

        if (isP1)
        {
            characters.transform.localScale = new Vector3(2.4f, 3.1f, 1);
            characters.transform.position = new Vector3(-6, -1.98f, 0);
            characters.transform.Rotate(0, 180, 0);
        }
        else if(isP2)
        {
            characters.transform.localScale = new Vector3(2.4f, 3.1f, 1);
            characters.transform.position = new Vector3(6, -1.8f, 0);
            characters.transform.Rotate(0, 180, 0);
        }
    }

    void changeDirection(string direction)
    {
        if (_currentDirection != direction)
        {
            if (direction == "right")
            {
                characters.transform.Rotate(0, 180, 0);
                _currentDirection = "right";
            }
            else if (direction == "left")
            {
                characters.transform.Rotate(0, -180, 0);
                _currentDirection = "left";
            }
        }
    }
}
