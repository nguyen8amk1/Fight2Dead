using SocketServer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelect : MonoBehaviour
{
    private float fadeOutDuration = 2f;
    private float fadeInDuration = 2f;
    public CanvasGroup mapGroup;
    public static int selectVal = 0;
    public GameObject map,map0, map1, map2, map3, map4;
    public GameObject pointer0, pointer1, pointer2, pointer3, pointer4;
    private string[] mapName = new string[] {"Yoshi", "Sunny", "Palutena", "Fourside", "Temple" };
    private GameState globalGameState = GameState.Instance;
    private bool allPlayersChosen = false;

    private IEnumerator FadeOutMap(GameObject map)
    {
        float startAlpha = 1;
        float elapsedTime = 0f;
        float targetAlpha = 0f;

        while (elapsedTime < fadeOutDuration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeOutDuration);
            mapGroup.alpha = newAlpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mapGroup.alpha = 0f;
        
    }

    private IEnumerator FadeInMap()
    {
        float startAlpha = mapGroup.alpha;
        float elapsedTime = 0f;
        float targetAlpha = 1f; 

        while (elapsedTime < fadeInDuration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeInDuration);
            mapGroup.alpha = newAlpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mapGroup.alpha = 1f;

    }

    public void Map0()
    {
        selectVal = 0;
    }
    public void Map1()
    {
        selectVal = 1;
    }
    public void Map2()
    {
        selectVal = 2;
    }
    public void Map3()
    {
        selectVal = 3;
    }
    public void Map4()
    {
        selectVal = 4;
    }

    private void FadingMap()
    {
        if (selectVal == 0)
        {
            mapGroup.alpha = 0;          
            map0.SetActive(true);
            StartCoroutine(FadeInMap());
            map1.SetActive(false);
            map2.SetActive(false);
            map3.SetActive(false);
            map4.SetActive(false);

        }
        else if (selectVal == 1)
        {
            mapGroup.alpha = 0;
            map1.SetActive(true);
            StartCoroutine(FadeInMap());
            map0.SetActive(false);
            map2.SetActive(false);
            map3.SetActive(false);
            map4.SetActive(false);

        }
        else if (selectVal == 2)
        {
            mapGroup.alpha = 0;
            map2.SetActive(true);
            StartCoroutine(FadeInMap());
            map1.SetActive(false);
            map0.SetActive(false);
            map3.SetActive(false);
            map4.SetActive(false);

        }
        else if (selectVal == 3)
        {
            mapGroup.alpha = 0;
            map3.SetActive(true);
            StartCoroutine(FadeInMap());
            map1.SetActive(false);
            map2.SetActive(false);
            map0.SetActive(false);
            map4.SetActive(false);

        }
        else if (selectVal == 4)
        {
            mapGroup.alpha = 0;
            map4.SetActive(true);
            StartCoroutine(FadeInMap());
            map1.SetActive(false);
            map2.SetActive(false);
            map3.SetActive(false);
            map0.SetActive(false);
        }
    }
    private void OnApplicationQuit()
    {
        Debug.Log("Send quit message from map choose scene");
        string quitMessage = PreGameMessageGenerator.quitMessage();
        ServerCommute.connection.sendToServer(quitMessage);
    }

    private void Update()
    {
        if (globalGameState.someoneChooseMap)
        {
            Util.toNextScene();
        }

        if (globalGameState.onlineMode.Equals("LAN"))
        {
            if (globalGameState.lobby_P1Quit)
            {
                Debug.Log("TODO: remove the P1 on screen");
            }

            if (globalGameState.lobby_P2Quit)
            {
                Debug.Log("TODO: remove the P2 on screen");
            }
        }
        else if (globalGameState.onlineMode.Equals("GLOBAL"))
        {
            if (globalGameState.lobby_P1Quit ||
                globalGameState.lobby_P2Quit)
            {
                Debug.Log("Go back to menu");
                Util.toSceneWithIndex(globalGameState.scenesOrder["MENU"]);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            mapGroup.alpha = 0;
            selectVal = (selectVal + 1) % 5;
            FadingMap();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mapGroup.alpha = 0;
            selectVal = (selectVal - 1 + 5) % 5;
            FadingMap();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mapGroup.alpha = 0;
            selectVal = (selectVal - 2 + 5) % 5;
            FadingMap();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mapGroup.alpha = 0;
            selectVal = (selectVal + 2) % 5;
            FadingMap();
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(mapName[selectVal]);
            string message = PreGameMessageGenerator.chooseMapMessage(mapName[selectVal]);
            ServerCommute.connection.sendToServer(message);

            Debug.Log($"TODO: Send this message to server: {message}");
        }    

        if (selectVal == 0)
        {
            pointer0.SetActive(true);
        }
        else pointer0.SetActive(false);

        if (selectVal == 1)
        {
            pointer1.SetActive(true);
        }
        else pointer1.SetActive(false);

        if (selectVal == 2)
        {
            pointer2.SetActive(true);
        }
        else pointer2.SetActive(false);

        if (selectVal == 3)
        {
            pointer3.SetActive(true);
        }
        else pointer3.SetActive(false);

        if (selectVal == 4)
        {
            pointer4.SetActive(true);
        }
        else pointer4.SetActive(false);
      
    }
}
