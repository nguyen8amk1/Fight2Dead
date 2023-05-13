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
using SocketServer;

public class LobbyGetState : MonoBehaviour
{

    private bool ready = false;
    public static int count = 1;

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

	}

	private void OnApplicationQuit()
	{
		//RoomMessageHandler.sendCloseConnection();
		Debug.Log("TODO: send quit message from lobby");
	}

	public void readyIsChosen()
	{
        ready = !ready;

		string message = PreGameMessageGenerator.lobbyReadyMessage(ready);
		ServerCommute.connection.sendToServer(message);
		Debug.Log($"send lobby ready/not ready message: {message}");

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

	private Color[] colors = new Color[] {
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
		if(globalGameState.opponentReady)
		{
			Debug.Log("the opponent is ready");
			count = 0;
		}

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


            if(globalGameState.opponentReady)
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
		}
        
    }

	private bool allPlayerReady()
	{
		return ready && globalGameState.opponentReady;
	}
}
