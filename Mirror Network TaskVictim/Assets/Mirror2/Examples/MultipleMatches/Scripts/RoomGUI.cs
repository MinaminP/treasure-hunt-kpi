using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Examples.MultipleMatch
{
    public class RoomGUI : MonoBehaviour
    {
        public GameObject playerList;
        public GameObject playerPrefab;
        public GameObject cancelButton;
        public GameObject leaveButton;
        public Button startButton;
        public bool owner;
        public bool justJoined;

        public int playerRed;
        public int playerBlue;

        public int updatedPlayerRed;
        public int updatedPlayerBlue;

        public Button redTeamButton;
        public Button blueTeamButton;

        public GameObject timerContainer;

        public Button timerDefine5;
        public Button timerDefine10;
        public Button timerDefine15;

        public bool isAlreadyTeamed = false;
        public Button readyButton;

        public void changetimer(float timer)
        {
            LocalPlayerData.gametimer = timer;
        }

        [ClientCallback]
        public void RefreshRoomPlayers(PlayerInfo[] playerInfos)
        {
            foreach (Transform child in playerList.transform)
                Destroy(child.gameObject);

            startButton.interactable = false;
            bool everyoneReady = true;

            //bool fakeReady = true;

            foreach (PlayerInfo playerInfo in playerInfos)
            {
                GameObject newPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                newPlayer.transform.SetParent(playerList.transform, false);
                newPlayer.GetComponent<PlayerGUI>().SetPlayerInfo(playerInfo);
                //playerInfo.playerTeam = "";
                //playerInfos[playerInfo.playerIndex].playerTeam = "";
                //lokasi awal

                /*if (everyoneReady)
                {
                    if (playerRed < playerInfos.Length)
                    {
                        if (playerInfo.playerTeam == "Red")
                        {
                            playerRed++;
                            if (playerBlue != 0)
                            {
                                playerBlue--;
                            }
                        }

                    }

                    if (playerBlue < playerInfos.Length)
                    {
                        if (playerInfo.playerTeam == "Blue")
                        {
                            playerBlue++;
                            if (playerRed != 0)
                            {
                                playerRed--;
                            }
                        }
                    }
                }*/
                
                /*if (playerRed < playerInfos.Length)
                {
                    if (LocalPlayerData.playerTeam == "Red")
                    {
                        playerRed++;
                        if (playerBlue != 0)
                        {
                            playerBlue--;
                        }
                    }
                }

                if (playerBlue < playerInfos.Length)
                {
                    if (LocalPlayerData.playerTeam == "Blue")
                    {
                        playerBlue++;
                        if (playerRed != 0)
                        {
                            playerRed--;
                        }
                    }
                }*/



                if (!playerInfo.ready || playerInfo.playerTeam == " ")
                {
                    everyoneReady = false;
                }

                
            }

            readyButton.interactable = isAlreadyTeamed;

            startButton.interactable = everyoneReady && owner && (playerInfos.Length > 1) && 
                (playerRed == playerInfos.Length / 2 || playerRed == playerInfos.Length / 2 + 1) && (playerBlue == playerInfos.Length / 2 || playerBlue == playerInfos.Length / 2 + 1);
        }

        [ClientCallback]
        public void SetOwner(bool owner)
        {
            this.owner = owner;
            LocalPlayerData.isOwner = owner;
            cancelButton.SetActive(owner);
            leaveButton.SetActive(!owner);
            timerContainer.gameObject.SetActive(owner);
        }

        [ClientCallback]
        public void addReds()
        {
            if (!justJoined)
            {
                playerBlue--;
                if (playerBlue <= 0)
                {
                    playerBlue = 0;
                }
                
            }
            playerRed++;
            justJoined = false;
            isAlreadyTeamed = true;

        }

        [ClientCallback]
        public void addBlues()
        {
            
            
            if (!justJoined)
            {
                playerRed--;
                if (playerRed <= 0)
                {
                    playerRed = 0;
                }
                
            }
            playerBlue++;
            justJoined = false;
            isAlreadyTeamed = true;

        }

        public void SetTeam(string team)
        {
            LocalPlayerData.playerTeam = team;
            if(team == "Red")
            {
               
                redTeamButton.interactable = false;
                blueTeamButton.interactable = true;
            }
            if (team == "Blue")
            {
                
                blueTeamButton.interactable = false;
                redTeamButton.interactable= true;
            }
        }
    }
}
