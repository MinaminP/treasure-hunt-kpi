using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace Mirror.Examples.MultipleMatch
{
    public class RoomGUI : NetworkBehaviour
    {
        public GameObject playerList;
        public GameObject playerPrefab;
        public GameObject cancelButton;
        public GameObject leaveButton;
        public Button startButton;
        public bool owner;

        [SyncVar(hook = "UpdateLocalName")] public string localName;

        private void Start()
        {
            //localName = LocalPlayerData.playerUserName;
            //changeName();
        }

        [Command]
        public void changeName()
        {
            localName = LocalPlayerData.playerUserName;
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
                    everyoneReady = false;
            }

            startButton.interactable = everyoneReady && owner && (playerInfos.Length > 1);
        }

        [ClientCallback]
        public void SetOwner(bool owner)
        {
            this.owner = owner;
            cancelButton.SetActive(owner);
            leaveButton.SetActive(!owner);
        }

        public void UpdateLocalName(string oldName, string newName)
        {
            Debug.Log("wii");
        }

        public void setBlue()
        {
            LocalPlayerData.playerTeam = "Blue";

            //bluButton.SetActive(false);
            //redButton.SetActive(false);
        }

        public void setRed()
        {
            LocalPlayerData.playerTeam = "Red";

            //bluButton.SetActive(false);
            //redButton.SetActive(false);
        }
    }
}
