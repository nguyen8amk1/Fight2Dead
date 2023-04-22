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

    // RULES: 
    // left team1, right team2 

    // p1, p2 -> left -- p1 top, p2 bottom 
    // p3, p4 -> right -- p3 top, p4 bottom

    // TODO: accessing game assets using script 
    // @Test
    Dictionary<string, Sprite> allPlayerSprites = new Dictionary<string, Sprite>();

    // 24 sprites :)), no fun at all 
    public Sprite IshidaTopLeft;
    public Sprite IshidaTopRight;
    public Sprite IshidaBottomLeft;
    public Sprite IshidaBottomRight;

    public Sprite VenomTopLeft;
    public Sprite VenomTopRight;
    public Sprite VenomBottomLeft;
    public Sprite VenomBottomRight;

    public Sprite CapATopLeft;
    public Sprite CapATopRight;
    public Sprite CapABottomLeft;
    public Sprite CapABottomRight;

    public Sprite RyuTopLeft;
    public Sprite RyuTopRight;
    public Sprite RyuBottomLeft;
    public Sprite RyuBottomRight;

    public Sprite KenTopLeft;
    public Sprite KenTopRight;
    public Sprite KenBottomLeft;
    public Sprite KenBottomRight;

    // imagin this array gonna be given by the last scene  
    private string[] chosenCharacters = new string[] {
        "ken",
        "capa", 
        "venom",
        "ryu" 
    };

    private GameObject[] ps;

	void Start()
    {
        Application.targetFrameRate = 60;

        initPlayerSprites();
        matchPlayerWithTheRightSprite();

        initBackground();
        initVSFrames();

        initMovingGameObjectsPosition();
        hideNeccesaryGameObject();
    }

	private void hideNeccesaryGameObject()
	{

        V.SetActive(false);
        S.SetActive(false);
        VF.SetActive(false);
        SF.SetActive(false);

        p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(false);
        p4.SetActive(false);
	}

	private void initMovingGameObjectsPosition()
	{
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
	}

	private void initVSFrames()
	{
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
	}

	private void initBackground()
	{
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
	}

	private void matchPlayerWithTheRightSprite()
	{
		ps = new GameObject[] {
				p1, 
				p2, 
				p3, 
				p4 
		};

        for(int i = 0; i < chosenCharacters.Length; i++)
		{
            string name = chosenCharacters[i];
            string key = constructCharacterKeyName(name, i);
            Sprite sprite = allPlayerSprites[key];
            setPlayerSprite(i, sprite);
		}
	}

	private void initPlayerSprites()
	{
        allPlayerSprites.Add("ishida_top_left", IshidaTopLeft);
        allPlayerSprites.Add("ishida_bottom_left", IshidaBottomLeft);
        allPlayerSprites.Add("ishida_top_right", IshidaTopRight);
        allPlayerSprites.Add("ishida_bottom_right", IshidaBottomRight);

        allPlayerSprites.Add("capa_top_left", CapATopLeft);
        allPlayerSprites.Add("capa_bottom_left", CapABottomLeft);
        allPlayerSprites.Add("capa_top_right", CapATopRight);
        allPlayerSprites.Add("capa_bottom_right", CapABottomRight);

        allPlayerSprites.Add("ken_top_left", KenTopLeft);
        allPlayerSprites.Add("ken_bottom_left", KenBottomLeft);
        allPlayerSprites.Add("ken_top_right", KenTopRight);
        allPlayerSprites.Add("ken_bottom_right", KenBottomRight);

        allPlayerSprites.Add("venom_top_left", VenomTopLeft);
        allPlayerSprites.Add("venom_bottom_left", VenomBottomLeft);
        allPlayerSprites.Add("venom_top_right", VenomTopRight);
        allPlayerSprites.Add("venom_bottom_right", VenomBottomRight);

        allPlayerSprites.Add("ryu_top_left", RyuTopLeft);
        allPlayerSprites.Add("ryu_bottom_left", RyuBottomLeft);
        allPlayerSprites.Add("ryu_top_right", RyuTopRight);
        allPlayerSprites.Add("ryu_bottom_right", RyuBottomRight);
	}

	private void setPlayerSprite(int i, Sprite sprite)
	{
		ps[i].GetComponent<SpriteRenderer>().sprite = sprite;
	}

	private string constructCharacterKeyName(string name, int index)
	{
        // so the order is:  
        // top left, bottom left, top right, bottom right 
        string[] locs = new string[] { 
            "top_left", 
            "bottom_left", 
            "top_right", 
            "bottom_right" 
        };
        return $"{name}_{locs[index]}";

        throw new Exception("Can't construct character keyname with index value = " + index); 
	}

	// Update is called once per frame
	void Update()
    {
        if(currentState == GlobalTimingStates.STATIC)
		{
            handleStaticState();
		}

        else if(currentState == GlobalTimingStates.QUAD_IN)
		{
            handleQuadInState();
		}

        else if(currentState == GlobalTimingStates.VS_IN)
		{
            handleVSInState();
		}
        else if(currentState == GlobalTimingStates.TO_VS_FILL)
		{
            handleToVSFillState();
		}
        else if(currentState == GlobalTimingStates.EXPLODE)
		{
            handleExplodeState();
		}
        else if(currentState == GlobalTimingStates.TRANSITION)
		{
            handleTransitionState();
		}

        timingVar += Time.deltaTime;
    }

	private void handleTransitionState()
	{
        // TODO: add some filter at the end of the scene 
        Debug.Log("NOW WE DO THE TRANSITION STATE");
	}

	private void handleExplodeState()
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

	private void handleToVSFillState()
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

	private void handleVSInState()
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

	private void handleQuadInState()
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

	private void handleStaticState()
	{
		float t = (float)(timingVar/staticDuration);

		if(t >= 1) { 
			timingVar = 0;
			currentState = GlobalTimingStates.QUAD_IN;
		}
	}
}