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

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float width = player1.transform.position.x - player2.transform.position.x;

        // TODO: change how the x, y works, both are temporary solution, won't work in general cases 
        x = player1.transform.position.x - (width)/2;
        y = Mathf.Max(player1.transform.position.y,player2.transform.position.y);

		cam.orthographicSize = (Mathf.Abs(width*(1.0f/cam.aspect))/2) + 3f;

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
}
