using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PrintButton : MonoBehaviour
{
    // TODO: gui thong tin nhan vat cho server 
    // format: "rid:{roomId},pid:{pid},ch:{CharacterName}"
    private PlayerInfo playerInfo = PlayerInfo.Instance;
    private ServerConnection connection = ServerConnection.Instance;

    string[] charName = new string[] { "Ryu", "Four Arms", "Heatblast", "Venom" };
    public void OnButtonClick()
    {
        string chosenCharacter = "SOS";
        if (MainCharacter.selectVal == 0)
            chosenCharacter=(charName[0]);
        else if (MainCharacter.selectVal == 1)
            chosenCharacter=(charName[1]);
        else if (MainCharacter.selectVal == 2)
            chosenCharacter=(charName[2]);
        else chosenCharacter=(charName[3]);

        string message = $"rid:{playerInfo.RoomId},s:ch,pid:{playerInfo.PlayerId},ch:{chosenCharacter}";
        Debug.Log(message);
        connection.sendToServer(message);
    }
}
