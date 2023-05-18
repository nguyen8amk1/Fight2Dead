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
					switch(pid) 
					{
						case 1:
                            if(stat == 1)
							{
								gameState.lobbyP1Ready = true;
								Debug.Log("player 1 is ready");
							}
                            else if(stat == 0)
							{
								gameState.lobbyP1Ready = false;
								Debug.Log("player 1 is not ready");
							}
							break;
						case 2:
                            if(stat == 1)
							{
								gameState.lobbyP2Ready = true;
								Debug.Log("player 2 is ready");
							}
                            else if(stat == 0)
							{
								gameState.lobbyP2Ready = false;
								Debug.Log("player 2 is not ready");
							}
							break;
						case 3:
                            if(stat == 1)
							{
								gameState.lobbyP3Ready = true;
								Debug.Log("player 3 is ready");
							}
                            else if(stat == 0)
							{
								gameState.lobbyP3Ready = false;
								Debug.Log("player 3 is not ready");
							}
							break;
						case 4:
                            if(stat == 1)
							{
								gameState.lobbyP4Ready = true;
								Debug.Log("player 4 is ready");
							}
                            else if(stat == 0)
							{
								gameState.lobbyP4Ready = false;
								Debug.Log("player 4 is not ready");
							}
							break;
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

				bool isLoginStatus = Util.getKeyFrom(tokens[0]).Equals("login");
				if(isLoginStatus)
				{
					string status = Util.getValueFrom(tokens[0]); 
					Debug.Log("TODO: handle login status: " + status);
					if(status.Equals("success"))
					{
						//Util.toNextScene();
						gameState.loginSuccess = true;
					} else if(status.Equals("failed"))
					{
						Debug.Log("TODO: display on screen that login failed");
					}
				}

				bool isRegistrationStatus = Util.getKeyFrom(tokens[0]).Equals("registration");
				if(isRegistrationStatus)
				{
					string status = Util.getValueFrom(tokens[0]); 
					Debug.Log("handle registration status: " + status);
					if(status.Equals("success"))
					{
						Debug.Log("TODO: display on screen that registeration success, and now can login");
					} else if(status.Equals("failed"))
					{
						Debug.Log("TODO: display on screen that registeration failed");
					}
				}


            };
            return messageHandler;
        }

        public static MessageHandlerLambda tempUDPListening()  {
            MessageHandlerLambda messageHandler = (string[] tokens) => {
				Debug.Log("TODO: handle udp listening");
				bool isPositionMessage = Util.getKeyFrom(tokens[0]).Equals("pid") &&
										 Util.getKeyFrom(tokens[1]).Equals("x") && 
										 Util.getKeyFrom(tokens[2]).Equals("y");
				if(isPositionMessage)
				{
					int pid = Int32.Parse(Util.getValueFrom(tokens[0]));
					float x = float.Parse(Util.getValueFrom(tokens[1]));
					float y = float.Parse(Util.getValueFrom(tokens[2]));

					Debug.Log($"Reiceve position x: {x}, y:{y}");
					gameState.playersPosition[pid-1].x = x;
					gameState.playersPosition[pid-1].y = y;
				}
            };
            return messageHandler;
        }
    } 
}