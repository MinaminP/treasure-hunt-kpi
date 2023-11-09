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

        public Button redTeamButton;
        public Button blueTeamButton;

        public GameObject timerContainer;

        public Button timerDefine5;
        public Button timerDefine10;
        public Button timerDefine15;

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

            foreach (PlayerInfo playerInfo in playerInfos)
            {
                GameObject newPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                newPlayer.transform.SetParent(playerList.transform, false);
                newPlayer.GetComponent<PlayerGUI>().SetPlayerInfo(playerInfo);

                if (!playerInfo.ready)
                {
                    everyoneReady = false;
                }

                if (everyoneReady)
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
                }
            }

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

        public void SetTeam(string team)
        {
            LocalPlayerData.playerTeam = team;
            justJoined = false;
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
