using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
TODO: 
	FIX THE CAMERA (2h) []
	currently what's the camera can do: 
				can somewhat (linear) zoom in and zoom out to show the 2 players 

			what the camera need to have
				better zooming -> log zoom (more zoom in when far and less zoom in when close) [X] not exactly the one but it works good now 
				better frameming(not show the blue part) [X]
*/


public class CameraControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float x = 0, y = 0;
    public GameObject player1, player2;
    public Camera cam;
	private float theLeftPlayerPosX;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float edgeGap = calculateGap(Mathf.Abs(player1.transform.position.x - player2.transform.position.x));
        float twoPlayersDistX = Mathf.Abs(player1.transform.position.x - player2.transform.position.x) + edgeGap;
        float twoPlayersDistY = Mathf.Abs(player1.transform.position.y - player2.transform.position.y);

        // TODO: change how the x, y works, both are temporary solution, won't work in general cases 
        // how should x, y works 
        // x should offset from whoever on the left 
		x = (Mathf.Min(player1.transform.position.x, player2.transform.position.x) + (twoPlayersDistX/2)) - edgeGap/2;

		cam.orthographicSize = (twoPlayersDistX/cam.aspect)/2;

        if(nearlyEqual(player1.transform.position.y, player2.transform.position.y, 0.1f))
		{
			y = player1.transform.position.y;
		} else
		{
			y = Mathf.Max(player1.transform.position.y,player2.transform.position.y) - (twoPlayersDistY/2);
		}


        float halfH = cam.orthographicSize;
        float halfW = halfH * cam.aspect;

        float camUpperBorder = y + halfH;
        float camBottomBorder = y - halfH;
        float camLeftBorder = x - halfW;
        float camRightBorder = x + halfW;

		if(camUpperBorder > 18f) 
		{
            y = 18f - halfH;
		} 
        if(camBottomBorder < -18f) 
		{
            y = -18f + halfH;
		} 
        if(camLeftBorder < -41f) 
		{
            x = -41f + halfW;
		} 
        if(camRightBorder > 35f) 
		{
            x = 35f - halfW;
		}

		transform.position = new Vector3(x, y, 0);

    }

	private float calculateGap(float distX)
	{
        // TODO: find some graph that works  
        return Mathf.Exp(-(distX/5 - 2.8f)) + 4f; 
	}

	public static bool nearlyEqual(float a, float b, float epsilon)
    {
        float absA = Mathf.Abs(a);
        float absB = Mathf.Abs(b);
        float diff = Mathf.Abs(a - b);

        if (a == b)
        { // shortcut, handles infinities
            return true;
        }
        else if (a == 0 || b == 0 || absA + absB < float.MinValue)
        {
            // a or b is zero or both are extremely close to it
            // relative error is less meaningful here
            return diff < (epsilon * float.MinValue);
        }
        else
        { // use relative error
            return diff / (absA + absB) < epsilon;
        }
    }
}
