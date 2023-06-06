using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    private bool isPlayer1Active = true;
    private bool isPlayer2Active = false;
    private Vector3 currentPlayerPosition;

    void Start(){
        player2.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
            SwitchPlayers();
        }
    }

    void SwitchPlayers()
    {
        if (isPlayer1Active)
        {
            currentPlayerPosition = player1.transform.position;

            isPlayer1Active = false;
            isPlayer2Active = true;
            player1.SetActive(false);
            player2.SetActive(true);
            player2.transform.position = currentPlayerPosition;
            Debug.Log("PLAYER 1 SWITCH TO PLAYER 2");
        }
        else if (isPlayer2Active)
        {
            currentPlayerPosition = player2.transform.position;

            isPlayer1Active = true;
            isPlayer2Active = false;
            player2.SetActive(false);
            player1.SetActive(true);
            player1.transform.position = currentPlayerPosition;
            Debug.Log("PLAYER 2 SWITCH TO PLAYER 1");
        }
    }
}
