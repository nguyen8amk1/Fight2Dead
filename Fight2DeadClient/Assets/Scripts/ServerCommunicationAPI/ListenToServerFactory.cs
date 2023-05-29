using System;
using System.IO;
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

					if (gameState.numPlayers == 2)
					{
						gameState.player1Name = Util.getValueFrom(tokens[2]);
						gameState.player2Name = Util.getValueFrom(tokens[3]);
					}
					else if (gameState.numPlayers == 4)
					{
						gameState.player1Name = Util.getValueFrom(tokens[2]);
						gameState.player2Name = Util.getValueFrom(tokens[3]);
						gameState.player3Name = Util.getValueFrom(tokens[4]);
						gameState.player4Name = Util.getValueFrom(tokens[5]);
					}

					Debug.Log("TODO: set all the player in the room names");
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
					if(pn == 0)
					{
						gameState.player1IsChosen_2v2 = true;
					}
					else if(pn == 2)
					{
						gameState.player3IsChosen_2v2 = true;
					}
				}

                bool isChosenMapMessage = Util.getKeyFrom(tokens[0]) == "pid" && 
												Util.getKeyFrom(tokens[1]) == "mn";
                if(isChosenMapMessage)
				{
                    gameState.someoneChooseMap = true;
					gameState.chosenMapName = Util.getValueFrom(tokens[1]);
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
					Debug.Log("handle login status: " + status);

					if(status.Equals("success"))
					{
						//Util.toNextScene();
						gameState.loginSuccess = true;
					} else if(status.Equals("failed"))
					{
						//Debug.Log("TODO: display on screen that login failed");
						gameState.loginSuccess = false;
						ChangeInput.count = 1;
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
				//Debug.Log("TODO: handle udp listening");
				/*
				bool isPositionMessage = Util.getKeyFrom(tokens[0]).Equals("pid") &&
										 Util.getKeyFrom(tokens[1]).Equals("x") && 
										 Util.getKeyFrom(tokens[2]).Equals("y");
				*/
				int pid = Int32.Parse(tokens[0]);
				float x = float.Parse(tokens[1]);
				float y = float.Parse(tokens[2]);
				int state = Int32.Parse(tokens[3]);
				int currentChar = Int32.Parse(tokens[4]);

			/*
				walk left(-1)
				walk right(1)
				jump(2)
				fall(-2)
				got hit(3)
				idle(0)
			*/
			if (gameState.numPlayers == 2)
			{
				if (pid == 1)
				{
					gameState.player1IsBeingControlled = true;
					gameState.player1State = state;
					if (currentChar != gameState.currentCharT1)
					{
						gameState.p1Transformed = true;
						gameState.currentCharT1 = currentChar;
					} else
					{
						gameState.p1Transformed = false;
					}
				}
				else if (pid == 2)
				{
					gameState.player2IsBeingControlled = true;
					gameState.player2State = state;
					if (currentChar != gameState.currentCharT2)
					{
						gameState.p2Transformed = true;
						gameState.currentCharT2 = currentChar;
					} else
					{
						gameState.p2Transformed = false;
					}
				}


				//@Test: for now state is gonna be char id 
				//Debug.Log($"TODO: handle Receive state: {state}");
				gameState.playersPosition[pid - 1].x = x;
				gameState.playersPosition[pid - 1].y = y;
			} else if (gameState.numPlayers == 4)
			{
					/*
					// TODO
					if(pid == 1)
					{
						gameState.player1IsBeingControlled = true;
						gameState.player1State = state;
						if(currentChar != gameState.currentCharT1)
						{
							gameState.p1Transformed = true;
							gameState.currentCharT1 = currentChar; 
						} else
						{
							gameState.p1Transformed = false;
						}
					}
					else if(pid == 2)
					{
						gameState.player2IsBeingControlled = true;
						gameState.player2State = state;
						if(currentChar != gameState.currentCharT2)
						{
							gameState.p2Transformed = true;
							gameState.currentCharT2 = currentChar; 
						} else
						{
							gameState.p2Transformed = false;
						}
					}
					*/

					//@Test: for now state is gonna be char id 
					//Debug.Log($"TODO: handle Receive state: {state}");

					// 1 vs 3
					// playerid = 1 -> lay cua player 3
					// playerid = 2 -> lay cua player1 va player 3
					// playerid = 3 -> lay cua player 1 
					// playerid = 4 -> lay cua player1 va player 3

					// 2 vs 4 
					// playerid = 2 -> lay cua player 4
					// playerid = 1 -> lay cua player 2 va player 4
					// playerid = 4 -> lay cua player 2
					// playerid = 3 -> lay cua player 2 va player 4





					if(gameState.currentTeam1Player == 2 && gameState.currentTeam2Player == 4)
					{
						if(gameState.PlayerId == 1)
						{
							if(pid == 2)
							{
								gameState.playersPosition[0].x = x;
								gameState.playersPosition[0].y = y;
							}
							if(pid == 4)
							{
								gameState.playersPosition[1].x = x;
								gameState.playersPosition[1].y = y;
							}
						}

						if(gameState.PlayerId == 3)
						{
							if(pid == 2)
							{
								gameState.playersPosition[0].x = x;
								gameState.playersPosition[0].y = y;
							}
							if(pid == 4)
							{
								gameState.playersPosition[1].x = x;
								gameState.playersPosition[1].y = y;
							}
						}

						if(gameState.PlayerId == 2)
						{
							if(pid == 4)
							{
								gameState.playersPosition[1].x = x;
								gameState.playersPosition[1].y = y;
							}
						}
						if(gameState.PlayerId == 4)
						{
							if(pid == 2)
							{
								gameState.playersPosition[0].x = x;
								gameState.playersPosition[0].y = y;
							}
						}

					}
					else if(gameState.currentTeam1Player == 1 && gameState.currentTeam2Player == 4)
					{
						// 1 vs 4 @
						// playerid = 1 -> lay cua player 4
						// playerid = 2 -> lay cua player 1 va player 4
						// playerid = 3 -> lay cua player 1 va player 4
						// playerid = 4 -> lay cua player 1

						if(gameState.PlayerId == 2)
						{
							if(pid == 1)
							{
								gameState.playersPosition[0].x = x;
								gameState.playersPosition[0].y = y;
							}
							if(pid == 4)
							{
								gameState.playersPosition[1].x = x;
								gameState.playersPosition[1].y = y;
							}
						}

						if(gameState.PlayerId == 3)
						{
							if(pid == 1)
							{
								gameState.playersPosition[0].x = x;
								gameState.playersPosition[0].y = y;
							}
							if(pid == 4)
							{
								gameState.playersPosition[1].x = x;
								gameState.playersPosition[1].y = y;
							}
						}

						if(gameState.PlayerId == 1)
						{
							if(pid == 4)
							{
								gameState.playersPosition[1].x = x;
								gameState.playersPosition[1].y = y;
							}
						}

						if(gameState.PlayerId == 4)
						{
							if(pid == 2)
							{
								gameState.playersPosition[0].x = x;
								gameState.playersPosition[0].y = y;
							}
						}

					}

					else if(gameState.currentTeam1Player == 1 && gameState.currentTeam2Player == 3)
					{
						if(gameState.PlayerId == 2)
						{
							if(pid == 1)
							{
								gameState.playersPosition[0].x = x;
								gameState.playersPosition[0].y = y;
							}
							if(pid == 3)
							{
								gameState.playersPosition[1].x = x;
								gameState.playersPosition[1].y = y;
							}
						}

						if(gameState.PlayerId == 4)
						{
							if(pid == 1)
							{
								gameState.playersPosition[0].x = x;
								gameState.playersPosition[0].y = y;
							}
							if(pid == 3)
							{
								gameState.playersPosition[1].x = x;
								gameState.playersPosition[1].y = y;
							}
						}
						if(gameState.PlayerId == 1)
						{
							if(pid == 3)
							{
								gameState.playersPosition[1].x = x;
								gameState.playersPosition[1].y = y;
							}
						}
						if(gameState.PlayerId == 3)
						{
							if(pid == 1)
							{
								gameState.playersPosition[0].x = x;
								gameState.playersPosition[0].y = y;
							}
						}

					}


					}
					else if(gameState.currentTeam1Player == 2 && gameState.currentTeam2Player == 3)
					{
						// TODO: 
						// 2 vs 3 @
						// playerid = 1 -> lay cua player 2 va player 3
						// playerid = 2 -> lay cua player 3 
						// playerid = 3 -> lay cua player 2 
						// playerid = 4 -> lay cua player 2 va player 3

						if(gameState.PlayerId == 1)
						{
							if(pid == 2)
							{
								gameState.playersPosition[0].x = x;
								gameState.playersPosition[0].y = y;
							}
							if(pid == 3)
							{
								gameState.playersPosition[1].x = x;
								gameState.playersPosition[1].y = y;
							}
						}

						if(gameState.PlayerId == 4)
						{
							if(pid == 2)
							{
								gameState.playersPosition[0].x = x;
								gameState.playersPosition[0].y = y;
							}
							if(pid == 3)
							{
								gameState.playersPosition[1].x = x;
								gameState.playersPosition[1].y = y;
							}
						}

						if(gameState.PlayerId == 2)
						{
							if(pid == 3)
							{
								gameState.playersPosition[1].x = x;
								gameState.playersPosition[1].y = y;
							}
						}

						if(gameState.PlayerId == 3)
						{
							if(pid == 2)
							{
								gameState.playersPosition[0].x = x;
								gameState.playersPosition[0].y = y;
							}
						}


					}



            };
            return messageHandler;
        }
    } 
}