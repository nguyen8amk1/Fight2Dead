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
        VS_IN,  // 7 frametime
        TO_VS_FILL,  // 1 frametime
        EXPLODE,  // 6 frametime
        TRANSITION // 10 frametime
	};

    private double staticDuration = ut(6);
	private double quadInDuration = ut(15); 
    private double vsInDuration = ut(7);
    private double vsFillDuration = ut(1);
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

    // VS Fill
    private Vector3 VFPos = new Vector3(-1.3357f, -2.0259f, 0);
    private Vector3 SFPos = new Vector3(1.4888f, -2.1177f, 0);

    private Vector3 VFScale = new Vector3(1.540453f, 1.540453f, 1.540453f);
    private Vector3 SFScale = new Vector3(1.501732f, 1.501732f, 1.501732f);
    
    // Explosion 
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;

    public GameObject V;
    public GameObject S;

    public GameObject VF;
    public GameObject SF;

    public GameObject Background;

    public Sprite staticFrame;

    public Sprite explosionFrame0;
    public Sprite explosionFrame1;
    public Sprite explosionFrame2;
    public Sprite explosionFrame3;
    public Sprite explosionFrame4;
    public Sprite explosionFrame5;
    public Sprite explosionFrame6;

    public Sprite VFrame0;
    public Sprite VFrame1;
    public Sprite VFrame2;
    public Sprite VFrame3;
    public Sprite VFrame4;
    public Sprite VFrame5;
    public Sprite VFrame6;

    public Sprite SFrame0;
    public Sprite SFrame1;
    public Sprite SFrame2;
    public Sprite SFrame3;
    public Sprite SFrame4;
    public Sprite SFrame5;
    public Sprite SFrame6;


    private Sprite[] explosionFrames;
    private Sprite[] VFrames;
    private Sprite[] SFrames;

    private double timingVar = 0;

    private GlobalTimingStates currentState = GlobalTimingStates.STATIC;

    // TODO: figure out a way to dynamically set the right character for the right corner
    // TEST ONLY
    private Sprite[] chosenCharacters = new Sprite[] {
    }; 

    void Start()
    {
        // configure the chosen character sprites; 


        Application.targetFrameRate = 60;
        Background.GetComponent<SpriteRenderer>().sprite = staticFrame;

		explosionFrames = new Sprite[] {
			explosionFrame0, 
			explosionFrame1, 
			explosionFrame2, 
			explosionFrame3, 
			explosionFrame4, 
			explosionFrame5, 
			explosionFrame6 
		};

		VFrames = new Sprite[] {
			VFrame0, 
			VFrame1, 
			VFrame2, 
			VFrame3, 
			VFrame4, 
			VFrame5, 
			VFrame6 
		};

		SFrames = new Sprite[] {
			SFrame0, 
			SFrame1, 
			SFrame2, 
			SFrame3, 
			SFrame4, 
			SFrame5, 
			SFrame6 
		};


        p1.transform.position = p1Src;
        p2.transform.position = p2Src;
        p3.transform.position = p3Src;
        p4.transform.position = p4Src;

        V.transform.position = VSrc;
        S.transform.position = SSrc;

        V.transform.localScale = VScaleSrc;
        S.transform.localScale = SScaleSrc;

        VF.transform.localScale = VFScale;
        SF.transform.localScale = SFScale;

        VF.transform.position = VFPos;
        SF.transform.position = SFPos;

        V.SetActive(false);
        S.SetActive(false);
        VF.SetActive(false);
        SF.SetActive(false);

        p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(false);
        p4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == GlobalTimingStates.STATIC)
		{
            float t = (float)(timingVar/staticDuration);

            if(t >= 1) { 
				timingVar = 0;
				currentState = GlobalTimingStates.QUAD_IN;
            }
		}

        else if(currentState == GlobalTimingStates.QUAD_IN)
		{
			p1.SetActive(true);
			p2.SetActive(true);
			p3.SetActive(true);
			p4.SetActive(true);

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
			V.SetActive(true);
			S.SetActive(true);

            float t = (float)(timingVar/vsInDuration);
			V.transform.localScale = Vector3.Lerp(VScaleSrc, VScaleDest, t);
			V.transform.position = Vector3.Lerp(VSrc, VDest, t);

			S.transform.localScale = Vector3.Lerp(SScaleSrc, SScaleDest, t);
			S.transform.position = Vector3.Lerp(SSrc, SDest, t);
            if(t >= 1) { 
				timingVar = 0;
				currentState = GlobalTimingStates.TO_VS_FILL;
            }

		}
        else if(currentState == GlobalTimingStates.TO_VS_FILL)
		{
            float t = (float)(timingVar/vsFillDuration);

            V.SetActive(false);
            S.SetActive(false);

			VF.SetActive(true);
			SF.SetActive(true);

            if(t >= 1) { 
				timingVar = 0;
				currentState = GlobalTimingStates.EXPLODE;
            }
		}
        else if(currentState == GlobalTimingStates.EXPLODE)
		{
            float t = (float)(timingVar/explodeDuration);

            int index = (int)Math.Floor(t*explosionFrames.Length);
            Debug.Log("t: " + t + " index: " + index);
            if(index < 7)
			{
				Sprite frame = explosionFrames[index];
				Background.GetComponent<SpriteRenderer>().sprite = frame;

                frame = VFrames[index];
                VF.GetComponent<SpriteRenderer>().sprite = frame;
                frame = SFrames[index];
                SF.GetComponent<SpriteRenderer>().sprite = frame;
			}

            if(t >= 1) { 
				timingVar = 0;
				currentState = GlobalTimingStates.TRANSITION;
            }
		}
        else if(currentState == GlobalTimingStates.TRANSITION)
		{
			// TODO: add some filter at the end of scene 

		}

        timingVar += Time.deltaTime;
    }
}
