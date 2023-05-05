using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyGetState : MonoBehaviour
{
    private ServerConnection connection = ServerConnection.Instance;

    private bool ready = false;
    private bool opponentReady = false;
    private int count = 1;

	// text component 
	public Image player1ReadyImg;
	public Image player2ReadyImg;

	public Sprite readySprite;
	public Sprite notReadySprite;

	public Image player1Avatar; 
	public Image player2Avatar;

	public GameObject player1NameObj;
	private TextMeshProUGUI player1NameText;

	public GameObject player2NameObj;
	private TextMeshProUGUI player2NameText;

	public GameObject readyObj;
	private TextMeshProUGUI readyText;

	public GameObject exitObj;
	private TextMeshProUGUI exitText;

	public Image exitBlackStrip;
	public Image readyBlackStrip;

	private Thread listenToServerThread;
	private GameState globalGameState = GameState.Instance;

	private int currentChoice = 0;
	private Color blackStripChosenColor = new Color(255, 255, 255, 255);

	// FIXME: this not chosen color is too dark 
	private Color blackStripNotChosenColor = new Color(0, 0, 0, 136);
	private Color notChosenColor = new Color(255, 255, 255);
	private Color chosenColor = new Color(0, 0, 0);

	public Image readyChosenBorderTop; 
	public Image readyChosenBorderBottom; 

	public Image exitChosenBorderTop; 
	public Image exitChosenBorderBottom; 

	private void Start()
	{
		player1NameText = player1NameObj.GetComponent<TextMeshProUGUI>();
		player2NameText = player2NameObj.GetComponent<TextMeshProUGUI>();

		// init names
		player1NameText.text = globalGameState.player1Name;
		player2NameText.text = globalGameState.player2Name;

		player1ReadyImg.sprite = notReadySprite;
		player2ReadyImg.sprite = notReadySprite;


		readyText = readyObj.GetComponent<TextMeshProUGUI>();
		exitText = exitObj.GetComponent<TextMeshProUGUI>();

		initListenToServerThread();
	}

	private void initListenToServerThread()
	{
		RoomMessageHandler.MessageHandlerLambda messageHandler = (string[] tokens) =>
		{
			// TODO: put any process of the tokens here
			// received format: "pid:{oppid},stat:{1}" 

			int stat = Int32.Parse(getValue(tokens[1]));
			opponentReady = stat == 1;
			count = 0;
		};

        listenToServerThread = new Thread(() => RoomMessageHandler.listenToServer(messageHandler));
        listenToServerThread.Start();
	}

	private void OnApplicationQuit()
	{
		RoomMessageHandler.sendCloseConnection();
	}

	public void readyIsChosen()
	{
        ready = !ready;

        RoomMessageHandler.sendLobbyMessage(ready);

        bool isPlayer1 = globalGameState.PlayerId == 1;
        bool isPlayer2 = globalGameState.PlayerId == 2;

		Image image = null;

        if(isPlayer1)
		{
			image = player1ReadyImg;
		} else if(isPlayer2)
		{
			image = player2ReadyImg;
		}

		if(ready) {
			changeStatus(image, true);
		} else
		{
			changeStatus(image, false);
		}
	}

	private void changeStatus(Image image, bool ready2Play)
	{
		if(ready2Play)
		{
			image.sprite = readySprite;
		} else
		{
			image.sprite = notReadySprite;
		} 
	}

	private string getValue(string s)
	{
        return s.Split(':')[1];
	}

	private Color[] colors = new Color[] {
	// TODO: 
		new Color(255, 0, 0),
		new Color(0, 255, 0),
		new Color(0, 0, 255)
	};
	private int currentColorIndex = 0;
	private int targetColorIndex = 1;
	private float targetPoint = 0;
	private float borderColorTransitionDuration = .5f;

	// Update is called once per frame
	void Update()
    {

		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			currentChoice = Math.Abs(--currentChoice) % 2;
			Debug.Log("Current choice is + " + currentChoice.ToString());
		}
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			currentChoice = (++currentChoice) % 2;
			Debug.Log("Current choice is + " + currentChoice.ToString());
		}
		if(Input.GetKeyDown(KeyCode.Return))
		{
			if(currentChoice == 0)
			{
				readyIsChosen();
				Debug.Log("Chosen NEXT SCENE");
			} 
			if(currentChoice == 1)
			{
				// TODO: back to the previous menu scene 
				Debug.Log("Chosen EXIT, back to the previous menu scene");
			}
		}

		// TODO: lighten which ever choice
		// what changed?? 
		// original state: white text, black background 
		// changed state: black text, white background, color lerp border
		if(currentChoice == 0)
		{
			targetPoint += Time.deltaTime/borderColorTransitionDuration;
			readyChosenBorderTop.color = Color.Lerp(colors[currentColorIndex], colors[targetColorIndex], targetPoint);
			readyChosenBorderBottom.color = Color.Lerp(colors[currentColorIndex], colors[targetColorIndex], targetPoint);
			if(targetPoint >= 1)
			{
				targetPoint -= 1;
				currentColorIndex = targetColorIndex;
				targetColorIndex++;
				if(targetColorIndex == colors.Length)
				{
					targetColorIndex = 0;
				}
			}

			readyChosenBorderTop.enabled = true;
			readyChosenBorderBottom.enabled = true;

			readyBlackStrip.color = blackStripChosenColor;
			readyText.color = chosenColor;
		} else
		{
			readyChosenBorderTop.enabled = false;
			readyChosenBorderBottom.enabled = false;
			readyBlackStrip.color = blackStripNotChosenColor;
			readyText.color = notChosenColor;
		}

		if(currentChoice == 1)
		{
			targetPoint += Time.deltaTime/borderColorTransitionDuration;
			exitChosenBorderTop.color = Color.Lerp(colors[currentColorIndex], colors[targetColorIndex], targetPoint);
			exitChosenBorderBottom.color = Color.Lerp(colors[currentColorIndex], colors[targetColorIndex], targetPoint);
			if(targetPoint >= 1)
			{
				targetPoint -= 1;
				currentColorIndex = targetColorIndex;
				targetColorIndex++;
				if(targetColorIndex == colors.Length)
				{
					targetColorIndex = 0;
				}
			}

			exitChosenBorderTop.enabled = true;
			exitChosenBorderBottom.enabled = true;
			exitBlackStrip.color = blackStripChosenColor;
			exitText.color = chosenColor;
		} else
		{
			exitChosenBorderTop.enabled = false;
			exitChosenBorderBottom.enabled = false;
			exitBlackStrip.color = blackStripNotChosenColor;
			exitText.color = notChosenColor;
		}


        if(count == 0)
		{
			bool isPlayer1 = globalGameState.PlayerId == 1;
			bool isPlayer2 = globalGameState.PlayerId == 2;

			Image image = null;

			if(isPlayer1)
			{
				image = player2ReadyImg;
			} else if(isPlayer2)
			{
				image = player1ReadyImg;
			}


            if(opponentReady)
			{
                changeStatus(image, true);
			}
            else
			{
                changeStatus(image, false);
			}
            count = 1;
		}

        if(allPlayerReady())
		{
            Util.toNextScene();
			listenToServerThread.Abort();
		}
        
    }

	private bool allPlayerReady()
	{
		return ready && opponentReady;
	}
}
