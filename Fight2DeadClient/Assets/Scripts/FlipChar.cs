using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipChar : MonoBehaviour
{   
    public string _currentDirection = "left";
    public GameObject characters;
    private bool isFlipped = false;
    // Start is called before the first frame update
    public void flipChar()
    {
        if (!isFlipped)
        {
            characters.transform.localScale = new Vector3(2.4f, 3.1f, 1);
            characters.transform.position = new Vector3(-7.95f, -1.98f, 0);
            characters.transform.Rotate(0, 180, 0);
            isFlipped = true;
        }
        else
        {
            characters.transform.localScale = new Vector3(2.4f, 3.1f, 1);
            characters.transform.position = new Vector3(6.7f, -1.8f, 0);
            characters.transform.Rotate(0, -180, 0);
            isFlipped = false;
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
