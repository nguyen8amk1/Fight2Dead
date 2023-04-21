using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // STATES 
    enum GlobalTimingStates
	{
        // here we use 1/24 frame time 
        STATIC,  // 6 frametime 
        QUAD_IN, // 15 frametime
        VS_IN,  // 8 frametime
        EXPLODE,  // 6 frametime
        TRANSITION // 10 frametime
	};

    private double staticDuration = ut(6);
	private double quadInDuration = ut(15); 
    private double vsInDuration = ut(8);
	private double explodeDuration = ut(6); 
	private double transitionDuration = ut(10); 

	private static double ut(int v)
	{
        return v*(1.0/24.0);
	}

    // 4 player
    private static Vector3 p1Src= new Vector3(12.12f, -4.98f, 0);
    private static Vector3 p2Src = new Vector3(9.74f, 6.77f, 0);
    private static Vector3 p3Src = new Vector3(-12.01f, -5.13f, 0);
    private static Vector3 p4Src = new Vector3(-12.01f, 5.8f, 0);

    // Start is called beforeishida the first frame update
    private static Vector3 p1Dest = new Vector3(-5.38f, 2.43f, 0);
    private static Vector3 p2Dest = new Vector3(-5.38f, -2.37f, 0);
    private static Vector3 p3Dest = new Vector3(5.39f, 2.38f, 0);
    private static Vector3 p4Dest = new Vector3(5.39f, -2.31f, 0);

    // VS 
    private static Vector3 VSrc = new Vector3(-9.09f, 0.55f, 0);
    private static Vector3 VDest = new Vector3(-1.8f, 0.1475f, 0);

	private static Vector3 SSrc = new Vector3(10.38f, 0.47f, 0);
    private static Vector3 SDest = new Vector3(1.95f, 0.11f, 0);

    // VS scale: 
    private Vector3 VScaleSrc = new Vector3(5.895488f, 5.870328f, 0);
    private Vector3 SScaleSrc = new Vector3(6.470812f, 6.470812f, 0);

    private Vector3 VScaleDest = new Vector3(1.877777f, 1.869763f, 0);
    private Vector3 SScaleDest = new Vector3(1.956057f, 1.956057f, 0);


    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;

    public GameObject V;
    public GameObject S;

    private double timingVar = 0;

    private GlobalTimingStates currentState = GlobalTimingStates.STATIC;

    void Start()
    {
        Application.targetFrameRate = 60;

        p1.transform.position = p1Src;
        p2.transform.position = p2Src;
        p3.transform.position = p3Src;
        p4.transform.position = p4Src;

        V.transform.position = VSrc;
        S.transform.position = SSrc;
    }

    // Update is called once per frame
    void Update()
    {
        //currentState = nextState(currentState, timingVar);
        //Debug.Log(currentState);
        if(currentState == GlobalTimingStates.STATIC)
		{
            float t = (float)(timingVar/staticDuration);
            if(t >= 1) { 
				timingVar = 0;
				currentState = GlobalTimingStates.QUAD_IN;
            }
		}

        // TODO: lerp the position instead of increase the position incrementally
        else if(currentState == GlobalTimingStates.QUAD_IN)
		{

            float t = (float)(timingVar/quadInDuration);
			p1.transform.position = Vector3.Lerp(p1Src, p1Dest, t);
			p2.transform.position = Vector3.Lerp(p2Src, p2Dest, t);
			p3.transform.position = Vector3.Lerp(p3Src, p3Dest, t);
			p4.transform.position = Vector3.Lerp(p4Src, p4Dest, t);

            if(t >= 1) { 
				timingVar = 0;
				currentState = GlobalTimingStates.VS_IN;
            }
		}

        else if(currentState == GlobalTimingStates.VS_IN)
		{
            float t = (float)(timingVar/vsInDuration);
			// FIXME: overlay problem 
			V.transform.localScale = Vector3.Lerp(VScaleSrc, VScaleDest, t);
			V.transform.position = Vector3.Lerp(VSrc, VDest, t);

			S.transform.localScale = Vector3.Lerp(SScaleSrc, SScaleDest, t);
			S.transform.position = Vector3.Lerp(SSrc, SDest, t);

            if(t >= 1) { 
				timingVar = 0;
				currentState = GlobalTimingStates.EXPLODE;
            }
		}
        else if(currentState == GlobalTimingStates.EXPLODE)
		{
            // TODO: 
		}
        else if(currentState == GlobalTimingStates.TRANSITION)
		{
            // TODO: 
		}

        timingVar += Time.deltaTime;
    }
}
