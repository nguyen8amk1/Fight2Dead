using SocketServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class LoadingSceneController : MonoBehaviour
{
    // CURRENTLY DOING: None

    // TODO: make the earth background animated (1h) [] 
    // find a moving stars field sprite sheet [X]
    // cut the earth out [X]
    // create a starfield gif [X]
    // put the gif on Unity with the earth image on top []

    // @Later
    // TODO: add some delay to simulate if server take a long time (2h) []
    // STATE ORDER
    // @Ideas: should we add to each scene 1 more frame time ?? 
    // -> total of 2s of loading screen 

    // static (delay) -> quad in -> vs in -> vs fill in -> explode -> transition orange background -> transition white background 
    // static scene is the delay 

    // @Later
    // TODO: add sound effects (1h) []
    // PLAY THE SOUND IN SEPERATE THREAD ?? 

    // @Later
    // TODO: find sound effects (2h) []
    // how many sounds are there: 
    // nhac delay: normal theme song 
    // 4ps in ->  nhac  (téo téo teo) [] (about 2s), force to wait for 2s ?? (VERSUS SOUNDTRACK)
    // tieng no -> tieng no chill (no "dung" nhung ma khong qua to) [] (boom sound effect)

    // tieng transition -> deo biet ?? (hien tai chua can) [] ()


    // -> chay xuyen suot ko duoc giat cuc -> gop lai thanh 1 sound, hoac choi cung 1 lan 
    // -> chay trong multiple scene 

    // STATES 
    enum GlobalTimingStates
    {
        // here we use 1/24 frame time 
        STATIC,  // 6 frametime 
        QUAD_IN, // 15 frametime
        VS_IN,  // 7 frametime
        TO_VS_FILL,  // 1 frametime
        EXPLODE,  // 6 frametime
        TRANSITION_ORANGE_BACKGROUND, // 1 frametime
        TRANSITION_WHITE_BACKGROUND // 9 frametime
    };

    private double staticDuration = ut(6);
    private double quadInDuration = ut(15);
    private double vsInDuration = ut(7);
    private double vsFillDuration = ut(1);
    private double explodeDuration = ut(6);
    private double transitionOrangeBgDuration = ut(1);
    private double transitionWhiteBgDuration = ut(9);

    private static double ut(int v)
    {
        return v * (1.0 / 24.0);
    }

    // 4 player
    private static Vector3 p1Src = new Vector3(12.12f, -4.98f, 0);
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

    // Background
    private Vector3 backgroundPos = new Vector3(0, 0, 0);
    private Vector3 backgroundScale = new Vector3(1.956057f, 1.956057f, 1.956057f);

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

    // VS fill frames 
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

    // @Test
    Dictionary<string, Sprite> allPlayerSprites = new Dictionary<string, Sprite>();

    // 24 sprites :)), no fun at all 
    public Sprite GaaraTopLeft;
    public Sprite GaaraTopRight;
    public Sprite GaaraBottomLeft;
    public Sprite GaaraBottomRight;

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

    public Sprite JotaroTopLeft;
    public Sprite JotaroTopRight;
    public Sprite JotaroBottomLeft;
    public Sprite JotaroBottomRight;

    public Sprite LinkTopLeft;
    public Sprite LinkTopRight;
    public Sprite LinkBottomLeft;
    public Sprite LinkBottomRight;

    public Sprite RebornTopLeft;
    public Sprite RebornTopRight;
    public Sprite RebornBottomLeft;
    public Sprite RebornBottomRight;

    public Sprite SasoriTopLeft;
    public Sprite SasoriTopRight;
    public Sprite SasoriBottomLeft;
    public Sprite SasoriBottomRight;

    public Sprite orangeBackgroundSprite;
    public Sprite whiteBackgroundSprite;

    public GameObject earth;

    // imagin this array gonna be given by the last scene  
    private string[] chosenCharacters; 
    private GameObject[] ps;
    private GameState globalGameState = GameState.Instance;

    void Start()
    {
        Application.targetFrameRate = 60;
        // get chosen player
        chosenCharacters = globalGameState.chosenCharacters;

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
        //Background.GetComponent<SpriteRenderer>().sprite = ;

        Background.transform.position = backgroundPos;
        Background.transform.localScale = backgroundScale;

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

        for (int i = 0; i < chosenCharacters.Length; i++)
        {
            string name = chosenCharacters[i];
            string key = constructCharacterKeyName(name, i);
            Sprite sprite = allPlayerSprites[key];
            setPlayerSprite(i, sprite);
        }
    }

    private void initPlayerSprites()
    {
        allPlayerSprites.Add("gaara_top_left", GaaraTopLeft);
        allPlayerSprites.Add("gaara_bottom_left", GaaraBottomLeft);
        allPlayerSprites.Add("gaara_top_right", GaaraTopRight);
        allPlayerSprites.Add("gaara_bottom_right", GaaraBottomRight);

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

        allPlayerSprites.Add("jotaro_top_left", JotaroTopLeft);
        allPlayerSprites.Add("jotaro_bottom_left", JotaroBottomLeft);
        allPlayerSprites.Add("jotaro_top_right", JotaroTopRight);
        allPlayerSprites.Add("jotaro_bottom_right", JotaroBottomRight);

        allPlayerSprites.Add("reborn_top_left", RebornTopLeft);
        allPlayerSprites.Add("reborn_bottom_left", RebornBottomLeft);
        allPlayerSprites.Add("reborn_top_right", RebornTopRight);
        allPlayerSprites.Add("reborn_bottom_right", RebornBottomRight);

        allPlayerSprites.Add("sasori_top_left", SasoriTopLeft);
        allPlayerSprites.Add("sasori_bottom_left", SasoriBottomLeft);
        allPlayerSprites.Add("sasori_top_right", SasoriTopRight);
        allPlayerSprites.Add("sasori_bottom_right", SasoriBottomRight);

        allPlayerSprites.Add("link_top_left", LinkTopLeft);
        allPlayerSprites.Add("link_bottom_left", LinkBottomLeft);
        allPlayerSprites.Add("link_top_right", LinkTopRight);
        allPlayerSprites.Add("link_bottom_right", LinkBottomRight);

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
        if (currentState == GlobalTimingStates.STATIC)
        {
            handleStaticState();
        }

        else if (currentState == GlobalTimingStates.QUAD_IN)
        {
            handleQuadInState();
        }

        else if (currentState == GlobalTimingStates.VS_IN)
        {
            handleVSInState();
        }
        else if (currentState == GlobalTimingStates.TO_VS_FILL)
        {
            handleToVSFillState();
        }
        else if (currentState == GlobalTimingStates.EXPLODE)
        {
            earth.SetActive(false);
            handleExplodeState();
        }
        else if (currentState == GlobalTimingStates.TRANSITION_ORANGE_BACKGROUND)
        {
            handleTransitionOrangeBackgroundState();
        }
        else if (currentState == GlobalTimingStates.TRANSITION_WHITE_BACKGROUND)
        {
            handleTransitionWhiteBackgroundState();
        }

        timingVar += Time.deltaTime;
    }

    private void handleTransitionOrangeBackgroundState()
    {
        hideNeccesaryGameObject();

        float t = (float)(timingVar / transitionOrangeBgDuration);
        changeBackground(orangeBackgroundSprite);

        if (t >= 1)
        {
            timingVar = 0;
            currentState = GlobalTimingStates.TRANSITION_WHITE_BACKGROUND;
        }
    }

    private void handleTransitionWhiteBackgroundState()
    {
        float t = (float)(timingVar / transitionWhiteBgDuration);
        changeBackground(whiteBackgroundSprite);

        if (t >= 1)
        {
            timingVar = 0;
            // move to next scene ;
            // currentState = GlobalTimingStates.TO_VS_FILL;
            Debug.Log("TO NEXT SCENE");

            Debug.Log("Transition to UDP right here");
            string message = PreGameMessageGenerator.toUDPMessage();
            ServerCommute.connection.sendToServer(message);
            

            Debug.Log("TODO: There are udp connection bug right here");
            if(globalGameState.onlineMode == "LAN")
			{
				UDPServerConnection.Instance.inheritPortFromLAN(LANTCPServerConnection.Instance); 
			}

            else if (globalGameState.onlineMode == "GLOBAL")
            {
                UDPServerConnection.Instance.inheritPortFromGLOBAL(TCPServerConnection.Instance);
            }

            ServerCommute.listenToServerThread.Abort();
            //TCPServerConnection.Instance.close();
            ServerCommute.connection = UDPServerConnection.Instance;

            Console.WriteLine("Started UDP listen to server thread");

            ServerCommute.listenToServerThread = ServerCommute.connection.createListenToServerThread(ListenToServerFactory.tempUDPListening());
            ServerCommute.listenToServerThread.Start();
            
            Util.toNextScene();
        }
    }

    private void changeBackground(Sprite sprite)
    {
        Background.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void handleExplodeState()
    {
        playExplodeSoundtrack();

        float t = (float)(timingVar / explodeDuration);

        int index = (int)Math.Floor(t * explosionFrames.Length);
        Debug.Log("t: " + t + " index: " + index);
        if (index < 7)
        {
            Sprite frame = explosionFrames[index];
            Background.GetComponent<SpriteRenderer>().sprite = frame;

            frame = VFrames[index];
            VF.GetComponent<SpriteRenderer>().sprite = frame;

            frame = SFrames[index];
            SF.GetComponent<SpriteRenderer>().sprite = frame;
        }

        // TODO: the last 3 frames darken to dark orange
        /*
        if(t >= )
		{

		}
        */

        if (t >= 1)
        {
            timingVar = 0;
            currentState = GlobalTimingStates.TRANSITION_ORANGE_BACKGROUND;
        }
    }

    private void playExplodeSoundtrack()
    {
        // TODO: play explode soundtrack
    }

    private void handleToVSFillState()
    {
        float t = (float)(timingVar / vsFillDuration);

        V.SetActive(false);
        S.SetActive(false);

        VF.SetActive(true);
        SF.SetActive(true);

        if (t >= 1)
        {
            timingVar = 0;
            currentState = GlobalTimingStates.EXPLODE;
        }
    }

    private void handleVSInState()
    {
        V.SetActive(true);
        S.SetActive(true);

        float t = (float)(timingVar / vsInDuration);
        V.transform.localScale = Vector3.Lerp(VScaleSrc, VScaleDest, t);
        V.transform.position = Vector3.Lerp(VSrc, VDest, t);

        S.transform.localScale = Vector3.Lerp(SScaleSrc, SScaleDest, t);
        S.transform.position = Vector3.Lerp(SSrc, SDest, t);

        if (t >= 1)
        {
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

        float t = (float)(timingVar / quadInDuration);
        p1.transform.position = Vector3.Lerp(p1Src, p1Dest, t);
        p2.transform.position = Vector3.Lerp(p2Src, p2Dest, t);
        p3.transform.position = Vector3.Lerp(p3Src, p3Dest, t);
        p4.transform.position = Vector3.Lerp(p4Src, p4Dest, t);

        if (t >= 1)
        {
            timingVar = 0;
            currentState = GlobalTimingStates.VS_IN;
        }

        playQuadInSoundtrack();
    }

    private void playQuadInSoundtrack()
    {
        // TODO: play quad in soundtrack
    }

    private void handleStaticState()
    {
        float t = (float)(timingVar / staticDuration);
        // @Note: still wait for 6 frames for this static duration 
        // even when all the clients have been good 

        // TODO: verify the connections of clients 
        // if all are good move on the the next state
        // while doing wait for verify 
        Debug.Log("THIS THE STATIC STATE");

        // @Test: for now just do the static duration = 20 frames -> testing the circle 
        animateWaitingCircle();


        if (t >= 1)
        {
            /*
			bool allPlayersGood = checkIfAllPlayersIsConnectedGood();
			if(allPlayersGood)
			{
			} else
			{
                // TODO: wait for another 5 seconds;
                // then if all still not good then fail to go to the next scene  
                staticDuration += ut(10);
			}
            */

            timingVar = 0;
            currentState = GlobalTimingStates.QUAD_IN;
        }
    }

    private bool checkIfAllPlayersIsConnectedGood()
    {
        Debug.Log("CHECK ALL PLAYERS CONNECTED GOOD");

        // @Test
        return false;
    }

    private void animateWaitingCircle()
    {
        // TODO: do the circle animation
        // find the circle animation gif []
        // display that on Unity []

        Debug.Log("ANIMATING THE WAITING CIRCLE");
    }
}
