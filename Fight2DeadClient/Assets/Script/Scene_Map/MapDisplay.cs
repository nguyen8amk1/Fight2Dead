using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Text mapName;
    [SerializeField] private Image mapImage;
    [SerializeField] private Button playButton;

    // TODO: send the message for map  
    // format: "rid:{roomId},stg:{},pid:{}"
    private GameState playerInfo = GameState.Instance;
    private ServerConnection connection = ServerConnection.Instance;

    public void DisplayMap(Map _newMap)
    {
        mapName.text = _newMap.mapName;
        mapImage.sprite = _newMap.mapImage;

        playButton.onClick.RemoveAllListeners();

        playButton.onClick.AddListener(() => {
            string message = $"rid:{playerInfo.RoomId},stg:{mapName.text},pid:{playerInfo.PlayerId}";
            connection.sendToServer(message);
            Debug.Log($"Send this message to server: {message}");
        });
    }
}
