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

	public Sprite readySprite;
	public Sprite notReadySprite;

	// text component 
	public Image player1ReadyImg;
	public Image player2ReadyImg;

	public Image player1Avatar; 
	public Image player2Avatar;

	public GameObject player1NameObj;
	private TextMeshProUGUI player1NameText;
	public GameObject player2NameObj;
	private TextMeshProUGUI player2NameText;

	public GameObject player3NameObj;
	private TextMeshProUGUI player3NameText;
	public Image player3ReadyImg;
	public Image player3Avatar;
	public Image player3BlackStrip; 


	public GameObject player4NameObj;
	private TextMeshProUGUI player4NameText;
	public Image player4ReadyImg;
	public Image player4Avatar;
	public Image player4BlackStrip; 

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

	// TODO: New structure to refactor 
	// change the pIready and update everything accordingly

	private void Start()
	{
		player1NameText = player1NameObj.GetComponent<TextMeshProUGUI>();
		player2NameText = player2NameObj.GetComponent<TextMeshProUGUI>();

		// init names
		player1NameText.text = globalGameState.player1Name;
		player2NameText.text = globalGameState.player2Name;

		player1ReadyImg.sprite = notReadySprite;
		player2ReadyImg.sprite = notReadySprite;

		// TODO: Deactive 3, 4 
		player3NameObj.SetActive(false);
		player4NameObj.SetActive(false);

		player3ReadyImg.enabled = false;
		player4ReadyImg.enabled = false;

		player3Avatar.enabled = false; 
		player4Avatar.enabled = false;

		player3BlackStrip.enabled = false;
		player4BlackStrip.enabled = false;

		if(globalGameState.numPlayers == 4) 
		{
			player3NameObj.SetActive(true);
			player4NameObj.SetActive(true);

			player3ReadyImg.enabled = true;
			player4ReadyImg.enabled = true;

			player3Avatar.enabled = true; 
			player4Avatar.enabled = true;

			player3BlackStrip.enabled = true;
			player4BlackStrip.enabled = true;

			player3NameText = player3NameObj.GetComponent<TextMeshProUGUI>();
			player4NameText = player4NameObj.GetComponent<TextMeshProUGUI>();

			player3NameText.text = globalGameState.player3Name;
			player4NameText.text = globalGameState.player4Name;

			player3ReadyImg.sprite = notReadySprite;
			player4ReadyImg.sprite = notReadySprite;
		}

		readyText = readyObj.GetComponent<TextMeshProUGUI>();
		exitText = exitObj.GetComponent<TextMeshProUGUI>();
	}

	private void OnApplicationQuit()
	{
		Debug.Log("Send quit message from lobby");
		string quitMessage = PreGameMessageGenerator.quitMessage();
		ServerCommute.connection.sendToServer(quitMessage);
	}

	public void readyIsChosen()
	{
		bool isPlayer1 = globalGameState.PlayerId == 1;
		bool isPlayer2 = globalGameState.PlayerId == 2;
		bool isPlayer3 = globalGameState.PlayerId == 3;
		bool isPlayer4 = globalGameState.PlayerId == 4;

		ready = !ready;
		if(isPlayer1)
		{
			globalGameState.lobbyP1Ready = ready;
		} else if(isPlayer2)
		{
			globalGameState.lobbyP2Ready = ready;
		} else if(isPlayer3)
		{
			globalGameState.lobbyP3Ready = ready;
		} else if(isPlayer4)
		{
			globalGameState.lobbyP4Ready = ready;
		}

		string message = PreGameMessageGenerator.lobbyReadyMessage(ready);
		ServerCommute.connection.sendToServer(message);
		Debug.Log($"send lobby ready/not ready message: {message}");
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
		// Handle scene changing 
		if(globalGameState.onlineMode.Equals("LAN"))
		{
			if(globalGameState.lobby_P1Quit)
			{
				Debug.Log("TODO: remove the P1 on screen");
			}

			if(globalGameState.lobby_P2Quit)
			{
				Debug.Log("TODO: remove the P2 on screen");
			}
		}
		else if(globalGameState.onlineMode.Equals("GLOBAL"))
		{
			if (globalGameState.lobby_P1Quit ||
				globalGameState.lobby_P2Quit)
			{
				Debug.Log("Go back to menu");
				Util.toSceneWithIndex(globalGameState.scenesOrder["MENU"]);
			}
		}

		choiceAnimation();

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

		if(globalGameState.lobbyP1Ready)
			changeStatus(player1ReadyImg, true);
		else 
			changeStatus(player1ReadyImg, false);

		if(globalGameState.lobbyP2Ready)
			changeStatus(player2ReadyImg, true);
		else 
			changeStatus(player2ReadyImg, false);

		if(globalGameState.lobbyP3Ready)
			changeStatus(player3ReadyImg, true);
		else 
			changeStatus(player3ReadyImg, false);

		if(globalGameState.lobbyP4Ready)
			changeStatus(player4ReadyImg, true);
		else 
			changeStatus(player4ReadyImg, false);

        if(allPlayerReady())
		{
            Util.toNextScene();
		}
        
    }

	private void choiceAnimation()
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
	}

	private bool allPlayerReady()
	{
		if(globalGameState.numPlayers == 2)
		{
			return globalGameState.lobbyP1Ready  && globalGameState.lobbyP2Ready;	
		}
		if(globalGameState.numPlayers == 4)
		{
			return  globalGameState.lobbyP1Ready  && globalGameState.lobbyP2Ready && 
					globalGameState.lobbyP3Ready && globalGameState.lobbyP4Ready;
		}
		throw new Exception($"Numplayers ({globalGameState.numPlayers}) not regcognize");
	}
}
