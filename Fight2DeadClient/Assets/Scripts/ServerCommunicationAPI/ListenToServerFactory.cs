using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SocketServer {
    public sealed class ListenToServerFactory {

        public delegate void MessageHandlerLambda(string[] tokens);
        private static GameState gameState = GameState.Instance; 

        public static MessageHandlerLambda tempTCPListening()  {
            MessageHandlerLambda messageHandler = (string[] tokens) => {
                // TODO: have a factory for each message type just like in the server
                // have a regex for each message 
                bool isRidPid = Util.getKeyFrom(tokens[0]) == "rid" && Util.getKeyFrom(tokens[1]) == "pid";
                if (isRidPid)
                {
					gameState.RoomId = Int32.Parse(Util.getValueFrom(tokens[0]));
					gameState.PlayerId = Int32.Parse(Util.getValueFrom(tokens[1]));
                    gameState.receiveRidPid = true;
                }

                bool isLobbyReadyMessage = Util.getKeyFrom(tokens[0]) == "pid" && Util.getKeyFrom(tokens[1]) == "stat";
                if(isLobbyReadyMessage)
				{
                    int pid = Int32.Parse(Util.getValueFrom(tokens[0]));
					int stat = Int32.Parse(Util.getValueFrom(tokens[1]));
                    LobbyGetState.count = 0; 
                    if(gameState.numPlayers == 2)
					{
						gameState.opponentReady = stat == 1;

					} else if (gameState.numPlayers == 4)
					{
						switch(pid) 
                        {
                            case 1:
                                gameState.lobbyP1Ready = true;
                                break;
                            case 2:
                                gameState.lobbyP2Ready = true;
                                break;
                            case 3:
                                gameState.lobbyP3Ready = true;
                                break;
                            case 4:
                                gameState.lobbyP4Ready = true;
                                break;
                        }
					}
				}

                bool isChosenCharacterMessage = Util.getKeyFrom(tokens[0]) == "pid" && 
												Util.getKeyFrom(tokens[1]) == "pn" && 
												Util.getKeyFrom(tokens[2]) == "cn";
                if(isChosenCharacterMessage)
				{
                    int pn = Int32.Parse(Util.getValueFrom(tokens[1])) - 1;
                    string cn = Util.getValueFrom(tokens[2]);
                    gameState.chosenCharacters[pn] = cn;
                    gameState.charNameCount++;
                }

                bool isChosenMapMessage = Util.getKeyFrom(tokens[0]) == "pid" && 
												Util.getKeyFrom(tokens[1]) == "mn";
                if(isChosenMapMessage)
				{
                    // TODO: this is just temporarily
                    gameState.opponentMapChosen = true; 
				}

                bool isLobbyQuitMessage =   Util.getKeyFrom(tokens[0]) == "pid" && 
									        tokens[1] == "quit";
                if(isLobbyQuitMessage)
				{
                    // TODO: this is just temporarily
                    int pid = Int32.Parse(Util.getValueFrom(tokens[0]));
                    if(pid == 1)
					{
						gameState.lobby_P1Quit = true; 
					}
                    if(pid == 2)
					{
						gameState.lobby_P2Quit = true; 
					}
				}
            };
            return messageHandler;
        }

        public static MessageHandlerLambda tempUDPListening()  {
            MessageHandlerLambda messageHandler = (string[] tokens) => {
                // TODO: 
                Console.WriteLine("TEMP UDP LISTENING");
            };
            return messageHandler;
        }
    } 
}