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

        int playerRed;
        int playerBlue;

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

                if (playerInfo.playerTeam == "Red")
                {
                    playerRed++;
                    if(playerBlue != 0)
                    {
                        playerBlue--;
                    }
                }

                if (playerInfo.playerTeam == "Blue")
                {
                    playerBlue++;
                    if (playerRed != 0)
                    {
                        playerRed--;
                    }
                }

                if (!playerInfo.ready || playerInfo.playerTeam == " ")
                {
                    everyoneReady = false;
                }
            }

            startButton.interactable = everyoneReady && owner && (playerInfos.Length > 1) && playerRed == playerInfos.Length / 2 && playerBlue == playerInfos.Length / 2;
        }

        [ClientCallback]
        public void SetOwner(bool owner)
        {
            this.owner = owner;
            cancelButton.SetActive(owner);
            leaveButton.SetActive(!owner);
        }

        public void SetTeam(string team)
        {
            LocalPlayerData.playerTeam = team;
        }
    }
}
